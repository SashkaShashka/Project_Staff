using Microsoft.EntityFrameworkCore;
using Project_Staff.BD.Mapping;
using StaffDBContext_Code_first;
using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Staff.BD
{
    static class DbManager
    {
        static string connectionString = ConnectionManager.GetConnectionString();

        static StaffContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StaffContext>();
            var options = optionsBuilder
                    .UseSqlServer(connectionString)
                    .Options;
            return new StaffContext(options);
        }

        public static List<Position> GetPositions()
        {
            using var context = CreateContext();
            return context.Positions.Select(position => PositionMapper.Map(position)).ToList();
        }

        public static List<Staff> GetStaff(IEnumerable<Position> loadedPosition)
        {
            using var context = CreateContext();
            var staff = context.Staff
                .Include(staff => staff.Positions)
                .ToList();

            /*foreach (var employee in staff) {
                foreach (var post in employee.Positions) {
                    context.Entry(post)
                        .Navigation("Position")
                        .Load();
                }
            }*/
            //var staff = staffDb.Select(employee => StaffMapper.Map(employee));


            var loadedStaff = new List<Staff>();

            foreach (var employee in staff)
            {
                loadedStaff.Add(StaffMapper.Map(employee));
                foreach (var post in employee.Positions)
                {
                    loadedStaff.Last().AddPost(loadedPosition.FirstOrDefault(p => p.Id == post.PositionId), post.Bet);
                }
            }

            return loadedStaff.ToList();
        }

        public static void UpdatePositions(IEnumerable<Position> positions)
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var positionsDb = context.Positions.AsNoTracking();

                foreach (var positionDb in positionsDb)
                {
                    var position = positions
                        .FirstOrDefault(_position => _position.Id != null && _position.Id == positionDb.Id);

                    if (position != null)
                    {
                        //position.Update(positionDb);
                        context.Positions.Update(PositionMapper.Map(position));
                    }
                    else
                    {
                        context.Positions.Remove(positionDb);
                    }
                }
                context.SaveChanges();

                var positionsToAdd = positions
                    .Where(position => context.Positions.Where(p => position.Id != null && p.Id == position.Id).FirstOrDefault() == null);

                foreach (var positionToAdd in positionsToAdd)
                {
                    var positionToAddDb = PositionMapper.Map(positionToAdd);
                    context.Positions.Add(positionToAddDb);
                    context.SaveChanges();
                    positionToAdd.Id = positionToAddDb.Id;
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Произошла ошибка при сохранении должностей в базу: " + e.Message);
                transaction.Rollback();
            }
        }

        public static void UpdateStaff(IEnumerable<Staff> staff)
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var staffDb = context.Staff
                    .Include(staff => staff.Positions)
                    .AsNoTracking()
                    .ToList();

                foreach (var employeeDb in staffDb)
                {
                    var employee = staff.FirstOrDefault(e => e.ServiceNumber == employeeDb.ServiceNumber);

                    if (employee != null)
                    {
                        context.Staff.Update(StaffMapper.Map(employee));
                        UpdatePosts(context, employee.Posts, employeeDb, false);
                    }
                    else
                    {
                        context.Staff.Remove(employeeDb);
                        foreach (var post in employeeDb.Positions)
                        {
                            context.Entry(post).State = EntityState.Deleted;
                        }
                    }
                }

                context.SaveChanges();

                var StaffToAdd = staff
                        .Where(staff => context.Staff.FirstOrDefault(e => e.ServiceNumber == staff.ServiceNumber) == null);
                foreach (var employee in StaffToAdd)
                {
                    var employeeDb = StaffMapper.Map(employee);
                    context.Staff.Add(employeeDb);
                    context.StaffPositions.AddRange(employee.Posts.Select(post => PostMapper.Map(post, employee.ServiceNumber)));
                    context.SaveChanges();
                }


                transaction.Commit();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Произошла ошибка при сохранении сотрудников в базу: " + ex.Message);
                transaction.Rollback();
            }
        }

        private static void UpdatePosts(StaffContext context, IEnumerable<Post> posts, StaffDbDto staff, bool delete)
        {
            foreach (var postDb in staff.Positions)
            {
                var post = posts.FirstOrDefault(p => p.Position.Id == postDb.PositionId);

                if (post != null && !delete)
                {
                    context.StaffPositions.Update(PostMapper.Map(post, staff.ServiceNumber));
                }
                else
                {
                    //context.StaffPositions.Remove(postDb); Не работает, не понимаю почему

                    context.Entry(postDb).State = EntityState.Deleted;
                }
            }

            context.SaveChanges();

            if (!delete)
            {
                var postsToAdd = posts
                    .Where(post => staff.Positions.Where(p => p.PositionId == post.Position.Id).FirstOrDefault() == null)
                    .Select(post => PostMapper.Map(post, staff.ServiceNumber));

                context.StaffPositions.AddRange(postsToAdd);
                context.SaveChanges();
            }
        }
    }
}
