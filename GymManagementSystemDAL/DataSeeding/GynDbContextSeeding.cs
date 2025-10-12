using GymManagementSystemDAL.contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.DataSeeding
{
    public static class GynDbContextSeeding
    {
        public static bool DataSeed(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();

                if (HasPlans && HasCategories) return false;
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Models.Category>("categories.json");
                    if (Categories.Any())
                    {
                        dbContext.Categories.AddRange(Categories);
                    }
                }
                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Models.Plan>("plans.json");
                    if (Plans.Any())
                    {
                        dbContext.Plans.AddRange(Plans);
                    }
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seedin Failed : {ex.Message}");
                return false;
            }


        }
        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            var filePath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            string Data= File.ReadAllText(filePath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>() ;


        }


    }
}
