using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseKeeper.Models;

namespace CourseKeeper.Services
{
    public class MockDataStore 
    {
        List<Term> terms;

        public MockDataStore()
        {
            terms = new List<Term>();
            var mockItems = new List<Term>
            {
                new Term {
                    Name = "First term",
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2019,06,30),
                    CourseList = new List<Course>()
                },
                new Term {
                    Name = "Second term",
                    StartDate = new DateTime(2019, 01, 01),
                    EndDate = new DateTime(2019,06,30),
                    CourseList = new List<Course>()
                }
            };

            foreach (var item in mockItems)
            {
                for (int i = 1; i < 7; i++)
                {
                    Course course = new Course();
                    course.Name = "Course " + i;
                    item.CourseList.Add(course);
                }
                terms.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Term item)
        {
            terms.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Term item)
        {
            var oldItem = terms.Where((Term arg) => arg.ID == item.ID).FirstOrDefault();
            terms.Remove(oldItem);
            terms.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> SaveItemAsync(Term item)
        {
            if (item.ID != null)
            {
                var oldItem = terms.Where((Term arg) => arg.ID == item.ID).FirstOrDefault();
                terms.Remove(oldItem);
                terms.Add(item);

                return await Task.FromResult(true);
            }
            else
            {
                terms.Add(item);

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> DeleteItemAsync(Term item)
        {
            var oldItem = terms.Where((Term arg) => arg.ID == item.ID).FirstOrDefault();
            terms.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Term> GetItemAsync(string id)
        {
            return await Task.FromResult(terms.FirstOrDefault(s => s.ID == id));
        }

        public async Task<List<Term>> GetItemsAsync()
        {
            return await Task.FromResult(terms);
        }
    }
}