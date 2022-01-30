using StaffDBContext_Code_first.Model.DTO;
using StaffDBContext_Code_first.Repositories;
using StaffWebApi.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Services
{
    public class StaffService
    {
        protected StaffRepository staffRepository;
        protected PositionRepository positionRepository;

        public StaffService(StaffRepository staffRepository, PositionRepository positionRepository)
        {
            this.staffRepository = staffRepository;
            this.positionRepository = positionRepository;
        }
        public async Task<IEnumerable<StaffApiDto>> GetAsync(string search, bool? sortDate)
        {
            var staffs = await staffRepository.GetAllAsync(search, sortDate);
            
            var positions = await positionRepository.GetAllAsync(null, null);

            var returnStaffs = new List<StaffApiDto>();

            foreach (var employee in staffs)
            {
                returnStaffs.Add(new StaffApiDto(employee, positions));
            }
            
            return returnStaffs.ToList();
        }

        public async Task<(StaffApiDto, Exception)> GetAsync(int serviceNumber)
        {
            if (serviceNumber < 1)
                return (null, new ArgumentException($"Табельный номер не может быть меньше единицы"));
            var staff = await staffRepository.GetAsync(serviceNumber);

            var positions = await positionRepository.GetAllAsync(null, null);
            if (staff == null)
            {
                return (null, new KeyNotFoundException($"Сотрудник с табельным номером {serviceNumber} не найден"));
            }
            return (new StaffApiDto(staff, positions), null);
        }

        public async Task<Exception> CreateAsync(StaffApiDto staff)
        {

            var staffToCreate = staff.Create();
            staffRepository.Create(staffToCreate);

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        public async Task<(StaffApiDto, Exception)> DeleteAsync(int serviceNumber)
        {

            var deletedStaff = await staffRepository.DeleteAsync(serviceNumber);
            if (deletedStaff == null)
            {
                return (null, new KeyNotFoundException($"Сотрудник с табельным номером {serviceNumber} не найден"));
            }

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return (null, new Exception("Произошла ошибка при сохранении данных", ex));
            }

            return (new StaffApiDto(deletedStaff), null);
        }
    }
}
