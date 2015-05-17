#region

using System.Collections.Generic;
using PriceTag.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

#endregion

namespace PriceTag.Service
{
    public interface ITagService : IService<Tag>
    {

    }

    public class TagService : Service<Tag>, ITagService
    {
        private readonly IRepositoryAsync<Tag> _repository;

        public TagService(IRepositoryAsync<Tag> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
