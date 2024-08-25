﻿using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Exceptions;

namespace AikoLearning.Core.Domain.Entities;

public sealed class Article : BaseEntity
{
    #region Properties
    public string Name { get; private set; }
    public int CategoryId { get; private set; }
    public int UserId { get; private set; }
    public string Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public byte[]? Content { get; private set; }
    public Category Category { get; set; }
    public User User { get; set; }
    #endregion

    #region Constructor
    public Article(
        string name, 
        int categoryId, 
        int userId,
        string description, 
        byte[] content,
        string? imageUrl
    )
    {
        Update(name, categoryId, userId, description, content, imageUrl);        
    }

    public Article(
        int id,
        string name, 
        int categoryId, 
        int userId,
        string description, 
        byte[] content,
        string? imageUrl
    )
    {
        DomainValidationException.When(id < 0, "id do artigo inválido!");
        ID = id;        
        Update(name, categoryId, userId, description, content, imageUrl);
    }
    #endregion

    #region Methods

    public void Update(
        string name, 
        int categoryId, 
        int userId,
        string description, 
        byte[] content,
        string? imageUrl
    )
    {
        Validade(name, categoryId, userId, description, content);
        
        Name = name;
        CategoryId = categoryId;
        UserId = userId;
        Description = description;
        Content = content; 
        ImageUrl = imageUrl;
    }

    private void Validade(
        string name, 
        int categoryId, 
        int userId,
        string description,
        byte[] content
    )
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Informe o nome!");
        DomainValidationException.When(name.Length < 3, "Nome inválido, é necessário ter no minimo 3 caracteres!");
        DomainValidationException.When(name.Length > 100, "Nome deve ser menor que 100 caracteres!");
        
        DomainValidationException.When(categoryId < 0, "Inofrme a categoria!");
        DomainValidationException.When(userId < 0, "Informe o autor!");

        DomainValidationException.When(description.Length > 1000, "Descrição deve ser menor que 1000 caracteres!");
        DomainValidationException.When(content == null || content.Length == 0, "Conteúdo não informado!");
    }
    #endregion
}
