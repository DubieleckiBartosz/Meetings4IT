﻿using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Users.Entities;
using Panels.Domain.Users.Exceptions;
using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly List<UserTechnology> _stack;
    private readonly List<Opinion> _opinions;
    public List<UserTechnology> TechStack => _stack;
    public UserImage? Image { get; private set; }
    public string Identifier { get; }
    public string Name { get; }

    /// <summary>
    /// Eager to go out
    /// </summary>
    public bool IsEager { get; private set; }

    public Email Email { get; private set; }
    public City City { get; private set; }

    private User()
    {
        _stack = new();
        _opinions = new();
    }

    private User(string identifier, string name, Email email, City city)
    {
        Identifier = identifier ?? throw new ArgumentNullException("User identifier cannot be null.");
        Name = name ?? throw new ArgumentNullException("Name cannot be null.");
        City = city ?? throw new ArgumentNullException("City cannot be null.");
        Email = email ?? throw new ArgumentNullException("Email cannot be null.");

        IsEager = false;

        _stack = new();
        _opinions = new();

        this.IncrementVersion();
    }

    public static User Create(
        string identifier,
        string name,
        Email email,
        City city)
    {
        return new User(identifier, name, email, city);
    }

    public void CompleteDetails(UserImage? image, List<Technology>? stack)
    {
        Image = image;

        if (stack != null && stack.Any())
        {
            foreach (var techItem in stack)
            {
                var userTech = new UserTechnology(techItem, this);

                _stack.Add(userTech);
            }
        }

        this.IncrementVersion();
    }

    public void AddNewTechnology(Technology technology)
    {
        var technologyAlreadyExists = _stack.FirstOrDefault(_ => _.Technology.Equals(technology));
        if (technologyAlreadyExists != null)
        {
            throw new TechnologyExistsException();
        }

        var userNewTech = new UserTechnology(technology, this);

        _stack.Add(userNewTech);
        this.IncrementVersion();
    }

    public void RemoveTechnology(string technology)
    {
        var tech = _stack.FirstOrDefault(_ => _.Technology.Value == technology);
        if (tech == null)
        {
            throw new TechnologyNotFoundException(technology);
        }

        _stack.Remove(tech);
        this.IncrementVersion();
    }

    public void SetProfileImage(UserImage image)
    {
        Image = image;
        this.IncrementVersion();
    }

    public void AddOpinion(string creatorId, string creatorName,
    Rating? ratingTechnicalSkills, Rating? ratingSoftSkills, Content? content)
    {
        var existingOpinion = _opinions.FirstOrDefault(_ => _.CreatorId == creatorId);
        if (existingOpinion != null)
        {
            throw new UserOpinionExistsException(this.Id, creatorId, existingOpinion.Id);
        }

        var newOpinion = Opinion.CreateNewOpinion(this.Id, creatorId, creatorName, ratingTechnicalSkills, ratingSoftSkills, content);

        _opinions.Add(newOpinion);
        this.IncrementVersion();
    }

    public void UpdateOpinion(
        int opinionId,
        string creatorId,
        Rating? ratingTechnicalSkills,
        Rating? ratingSoftSkills,
        Content? content)
    {
        var opinion = FindOpinionOrThrow(opinionId, creatorId);

        opinion.Update(ratingTechnicalSkills, ratingSoftSkills, content);

        var deleteOpinion = OpinionToDelete(opinion);
        if (deleteOpinion)
        {
            _opinions.Remove(opinion);
        }

        this.IncrementVersion();
    }

    public void DeleteOpinion(int opinionId, string deletedBy)
    {
        var opinion = FindOpinionOrThrow(opinionId, deletedBy);
        _opinions.Remove(opinion);
    }

    public void SetUserEagerToOpposite()
    {
        IsEager = !IsEager;
    }

    private Opinion FindOpinionOrThrow(int opinionId, string creatorOpinionId) =>
        _opinions.FirstOrDefault(_ => _.Id == opinionId && _.CreatorId == creatorOpinionId)
        ?? throw new OpinionNotFoundException(opinionId, this.Id, creatorOpinionId);

    private bool OpinionToDelete(Opinion opinion) => opinion.RatingSoftSkills == null && opinion.RatingTechnicalSkills == null && opinion.Content == null;
}