﻿using System;
using BlognoteApi.Models;
using MongoDB.Driver;

namespace BlognoteApi.Services
{
    public class AuthorService : ServiceBase<Author>
    {
        private readonly IMongoCollection<Article> articles;

        public AuthorService(IBlognoteDatabaseSettings settings) : base(settings, settings.AuthorsCollectionName)
        {
            articles = Database.GetCollection<Article>(settings.ArticlesCollectionName);
        }

        public void Remove(Author authorIn) =>
            Entities.DeleteOne(author => author.Id == authorIn.Id);

        public void Remove(string id) =>
            Entities.DeleteOne(author => author.Id == id);

        protected override void MapEntityProperties(Author author)
        {
            author.ArticlesCount = Convert.ToInt16(articles.CountDocuments(article => article.AuthorId == author.Id));
        }
    }
}
