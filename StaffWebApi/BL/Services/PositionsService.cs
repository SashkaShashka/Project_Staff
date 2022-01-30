using StaffDBContext_Code_first.Repositories;
using StaffWebApi.BL.Model;
using StaffWebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Services
{
    public class PositionsService
    {
        protected PositionRepository repository;

        public PositionsService(PositionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PositionApiDto>> GetAsync(string search, string filgerDivision)
        {
            var positions = await repository.GetAllAsync(search, filgerDivision);
            return positions.Select(p => new PositionApiDto(p)).ToList();
        }
        public async Task<(PositionApiDto, Exception)> GetAsync(int id)
        {
            if (id<1)
                return (null, new ArgumentException($"ID не может быть меньше единицы"));
            var position = await repository.GetAsync(id);
            if (position == null)
            {
                return (null, new KeyNotFoundException($"Должность с ID {id} не найдена"));
            }
            return (new PositionApiDto(position), null);
        }
        public async Task<Exception> CreateAsync(PositionApiDto position)
        {

            var productToCreate = position.Create();
            repository.Create(productToCreate);

            try
            {
                await repository.SaveAsync();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        public async Task<Exception> UpdateAsync(int id, PositionApiDto position)
        {
            if (id<0)
            {
                return new ArgumentException("Некорректный ID");
            }
            if (id != position.Id)
            {
                return new ConflictIdException();
            }


            var posotionToUpdate = await repository.GetAsync(id);
            if (posotionToUpdate == null)
            {
                return new KeyNotFoundException($"Должность со ID ${id} не найдена.");
            }
            position.Update(posotionToUpdate);
            repository.Update(posotionToUpdate);

            try
            {
                await repository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new Exception("Произошла ошибка при сохранении данных", ex);
            }
            return null;
        }
        public async Task<(PositionApiDto, Exception)> DeleteAsync(int id)
        {

            var deletedProduct = await repository.DeleteAsync(id);
            if (deletedProduct == null)
            {
                return (null, new KeyNotFoundException($"Должность с ID ${id} не найдена."));
            }

            try
            {
                await repository.SaveAsync();
            }
            catch (Exception ex)
            {
                return (null, new Exception("Произошла ошибка при сохранении данных", ex));
            }

            return (new PositionApiDto(deletedProduct), null);
        }
    }
}
