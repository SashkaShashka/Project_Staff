using Project_Staff;
using StaffDBContext_Code_first.Model.DTO;
using StaffDBContext_Code_first.Repositories;
using StaffWebApi.BL.Model;
using StaffWebApi.Exceptions;
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

        public async Task<string> GetTotalSalaryAsync()
        {
            var staffs = await GetAsync(null,null);
            return $"Расходы предприятия на зарплаты составляют : " + String.Format("{0:C2}", (double)staffs.Sum(p => p.Salary)/(1 - Staff.NDFL)) + " рублей";
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

        // изменение сотрудника - ФИО и BirthDay
        public async Task<Exception> UpdateAsync(int serviceNumber, StaffApiDto staff)
        {
            if (serviceNumber < 0)
            {
                return new ArgumentException("Некорректный ID");
            }
            if (serviceNumber != staff.ServiceNumber)
            {
                return new ConflictIdException();
            }


            var staffToUpdate = await staffRepository.GetAsync(serviceNumber);
            if (staffToUpdate == null)
            {
                return new KeyNotFoundException($"Cотрудник с табельным номером ${serviceNumber} не найден.");
            }
            staff.Update(staffToUpdate);
            staffRepository.Update(staffToUpdate);

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new Exception("Произошла ошибка при сохранении данных", ex);
            }
            return null;
        }

        // добавление списка должностей к уже имеюющимся по информации всей position с проверкой на существование у сотрудника
        public async Task<Exception> AddPositionAsync(int serviceNumber, StaffApiDto staff)
        {
            if (serviceNumber < 0)
            {
                return new ArgumentException("Некорректный ID");
            }
            if (serviceNumber != staff.ServiceNumber)
            {
                return new ConflictIdException();
            }


            var staffToUpdate = await staffRepository.GetAsync(serviceNumber);
            if (staffToUpdate == null)
            {
                return new KeyNotFoundException($"Cотрудник с табельным номером ${serviceNumber} не найден.");
            }
            var e = staff.AddPosition(staffToUpdate);
            if (e != null)
                return e;
            staffRepository.Update(staffToUpdate);

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new Exception("Произошла ошибка при сохранении данных", ex);
            }
            return null;
        }

        // удаление списка должностей из уже имеющихся по информации всей position с проверкой на существование у сотрудника
        public async Task<Exception> DeletePositionAsync(int serviceNumber, StaffApiDto staff)
        {
            if (serviceNumber < 0)
            {
                return new ArgumentException("Некорректный ID");
            }
            if (serviceNumber != staff.ServiceNumber)
            {
                return new ConflictIdException();
            }


            var staffToUpdate = await staffRepository.GetAsync(serviceNumber);
            if (staffToUpdate == null)
            {
                return new KeyNotFoundException($"Cотрудник с табельным номером ${serviceNumber} не найден.");
            }
            var e = staff.DeletePosition(staffToUpdate);
            if (e != null)
                return e;
            staffRepository.Update(staffToUpdate);

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new Exception("Произошла ошибка при сохранении данных", ex);
            }
            return null;
        }

        public async Task<Exception> UpdatePositionAsync(int serviceNumber, StaffApiDto staff)
        {
            if (serviceNumber < 0)
            {
                return new ArgumentException("Некорректный ID");
            }
            if (serviceNumber != staff.ServiceNumber)
            {
                return new ConflictIdException();
            }


            var staffToUpdate = await staffRepository.GetAsync(serviceNumber);
            if (staffToUpdate == null)
            {
                return new KeyNotFoundException($"Cотрудник с табельным номером ${serviceNumber} не найден.");
            }
            staff.UpdatePosition(staffToUpdate);
            staffRepository.Update(staffToUpdate);

            try
            {
                await staffRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new Exception("Произошла ошибка при сохранении данных", ex);
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
