
using AnkiOchAilesKlimatAPP.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Repositories
{
    public static class ObserverRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["dbAnkiochAile"].ConnectionString;
        #region CREATE

        //--------------------------OBSERVER-----------------------------//
        public static int AddObserver(Observer observer)
        {
            string stmt = "INSERT INTO observer(firstname, lastname) values(@firstname,@lastname) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("firstname", observer.FirstName);
                    command.Parameters.AddWithValue("lastname", observer.LastName);
                    int id = (int)command.ExecuteScalar();
                    observer.Id = id;
                    return id;
                }
            }
        }
        //--------------------------OBSERVER-----------------------------//

        //--------------------------OBSERVATION-----------------------------//

        public static int AddObservation(Observation observation)
        {
            string stmt = "INSERT INTO observation(obs_date) values(@obs_date) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("obs_date", observation.Date);
                    int id = (int)command.ExecuteScalar();
                    observation.Id = id;
                    return id;
                }
            }
        }

        //--------------------------OBSERVATION-----------------------------//

        //--------------------------GEOLOCATION-----------------------------//
        public static int AddGeolocation(Geolocation geolocation)
        {
            string stmt = "INSERT INTO geolocation(latitude, longitude) values(@latitude, @longitude) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("latitude", geolocation.Latitude);
                    command.Parameters.AddWithValue("longitude", geolocation.Longitude);
                    int id = (int)command.ExecuteScalar();
                    geolocation.Id = id;
                    return id;
                }
            }
        }
        //--------------------------GEOLOCATION-----------------------------//

        //--------------------------MEASUREMENT-----------------------------//
        public static int AddMeasurement(Measurement measurement)
        {
            string stmt = "INSERT INTO measurement(value) values(@value) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("value", measurement.Value);
                    int id = (int)command.ExecuteScalar();
                    measurement.Id = id;
                    return id;
                }
            }
        }

        //--------------------------MEASUREMENT-----------------------------//


        #endregion
        #region READ

        //--------------------------OBSERVER-----------------------------//
        public static Observer GetObserver(int id)
        {
            string stmt = "select id, firstname, lastname from observer where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observer observer = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            observer = new Observer
                            {

                                Id = (int)reader["id"],
                                FirstName = (string)reader["firstname"],
                                LastName = (string)reader["lastname"]

                            };

                        }

                    }
                }
                return observer;
            }

        }

        public static IEnumerable<Observer> GetObservers()
        {
            string stmt = "select id, firstname, lastname from observer";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observer observer = null;
                List<Observer> observers = new List<Observer>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            observer = new Observer
                            {

                                Id = (int)reader["id"],
                                FirstName = (string)reader["firstname"],
                                LastName = (string)reader["lastname"]

                            };
                            observers.Add(observer);

                        }

                    }
                }
                


                return observers;
            }
        }
        //--------------------------OBSERVER-----------------------------//

        //--------------------------OBSERVATION-----------------------------//
        public static Observation GetObservation(int id)
        {
            string stmt = "select id, obs_date, observer_id, geolocation_id from observation where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            observation = new Observation
                            {

                                Id = (int)reader["id"],
                                Date = (DateTime)reader["obs_date"],
                                ObserverId = (int)reader["observer_id"],
                                GeolocationId = (int)reader["geolocation_id"]

                            };

                        }

                    }
                }
                return observation;

            }

        }

        public static IEnumerable<Observation> GetObservations()
        {
            string stmt = "select id, obs_date, observer_id, geolocation_id from observation";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation observation = null;
                List<Observation> observations = new List<Observation>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            observation = new Observation
                            {

                                Id = (int)reader["id"],
                                Date = (DateTime)reader["obs_date"],
                                ObserverId = (int)reader["observer_id"],
                                GeolocationId = (int)reader["geolocation_id"]

                            };
                            observations.Add(observation);

                        }

                    }
                }
                return observations;
            }
        }
        //--------------------------OBSERVATION-----------------------------//

        //--------------------------GEOLOCATION-----------------------------//
        public static Geolocation GetGeolocation(int id)
        {
            string stmt = "select id, latitude, longitude, area_id from geolocation where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Geolocation geolocation = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            geolocation = new Geolocation
                            {

                                Id = (int)reader["id"],
                                Latitude = (double)reader["latitude"],
                                Longitude = (double)reader["longitude"],
                                AreaId = (int)reader["area_id"]

                            };

                        }

                    }
                }
                return geolocation;

            }

        }
        //--------------------------GEOLOCATION-----------------------------//

        //--------------------------MEASUREMENTS-----------------------------//
        public static Measurement GetMeasurement(int id)
        {
            string stmt = "select id, value, observation_id, category_id from measurement where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Measurement measurement = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            measurement = new Measurement
                            {

                                Id = (int)reader["id"],
                                Value = (double)reader["value"],
                                ObservationId = (int)reader["observation_id"],
                                CategoryId = (int)reader["category_id"]

                            };

                        }

                    }
                }
                return measurement;

            }
        }
        //--------------------------MEASUREMENTS-----------------------------//

        //--------------------------COUNTRY-----------------------------//

        public static Country GetCountry(int id)
        {
            string stmt = "select id, country from country where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Country country = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            country = new Country
                            {

                                Id = (int)reader["id"],
                                CountryName = (string)reader["country"]

                            };

                        }

                    }
                }
                return country;

            }

        }

        public static IEnumerable<Country> GetCountrys()
        {
            string stmt = "select id, country from country";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Country country = null;
                List<Country> countrys = new List<Country>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            country = new Country
                            {

                                Id = (int)reader["id"],
                                CountryName = (string)reader["country"],
                           
                            };
                            countrys.Add(country);

                        }

                    }
                }
                return countrys;
            }
        }

        //--------------------------COUNTRY-----------------------------//

        //--------------------------AREA-----------------------------//


        public static Area GetArea(int id)
        {
            string stmt = "select id, name, country_id from area where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Area area = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            area = new Area
                            {

                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                CountryId = (int)reader["country_id"]

                            };

                        }

                    }
                }
                return area;

            }

        }
        public static IEnumerable<Area> GetAreas()
        {
            string stmt = "select id, name, country_id from area";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Area area = null;
                List<Area> areas = new List<Area>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            area = new Area
                            {

                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                CountryId = (int)reader["country_id"]

                            };
                            areas.Add(area);

                        }

                    }
                }
                return areas;
            }
        }

        //--------------------------AREA-----------------------------//

        //--------------------------CATEGORY-----------------------------//

        public static Category GetCategory(int id)
        {
            string stmt = "select id, name, basecategory_id, unit_id from category where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category = null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            category = new Category
                            {

                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                BaseCategoryId = (int)reader["basecategory_id"],
                                UnitId = (int)reader["unit_id"]

                            };

                        }

                    }
                }
                return category;

            }

        }
        public static IEnumerable<Category> GetCategories()
        {
            string stmt = "select id, name, basecategory_id, unit_id from category";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Category category = null;
                List<Category> categories = new List<Category>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            category = new Category
                            {

                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                BaseCategoryId = (int)reader["basecategory_id"],
                                UnitId = (int)reader["unit_id"]

                            };
                            categories.Add(category);

                        }

                    }
                }
                return categories;
            }
        }
        //--------------------------CATEGORY-----------------------------//

        //--------------------------UNIT-----------------------------//
        public static Unit GetUnit(int id)
        {
            string stmt = "select id, type, abbreviation, from unit where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Unit unit= null;
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            unit = new Unit
                            {

                                Id = (int)reader["id"],
                                Type = (string)reader["type"],
                                Abbreviation = (string)reader["abbreviation"],
                            };

                        }

                    }
                }
                return unit;

            }

        }


        //--------------------------UNIT-----------------------------//


        #endregion
        #region UPDATE

        //--------------------------OBSERVER-----------------------------//
        public static void SaveObserver(Observer observer)
        {
            string stmt = "UPDATE observer set firstname = @firstname, @lastname where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("firstname", observer.FirstName);
                    command.Parameters.AddWithValue("lastname", observer.LastName);
                    command.ExecuteScalar();

                }
            }
        }

        //--------------------------OBSERVER-----------------------------//

        //--------------------------OBSERVATION-----------------------------//
        public static void SaveObservation(Observation observation)
        {
            string stmt = "UPDATE observation set obs_date = @obs_date where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("obs_date", observation.Date);
                    command.ExecuteScalar();

                }
            }
        }

        //--------------------------OBSERVATION-----------------------------//

        //--------------------------GEOLOCATION-----------------------------//
        public static void SaveGeolocation(Geolocation geolocation)
        {
            string stmt = "UPDATE geolocation set latitude = @latitude, longitude = @longitude where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("latitude", geolocation.Latitude);
                    command.Parameters.AddWithValue("longitude", geolocation.Longitude);
                    command.ExecuteScalar();

                }
            }
        }
        //--------------------------GEOLOCATION-----------------------------//

        //--------------------------MEASUREMENT-----------------------------//
        public static void SaveObservation(Measurement measurement)
        {
            string stmt = "UPDATE measurement set value = @value where id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("value", measurement.Value);
                    command.ExecuteScalar();

                }
            }
        }
        //--------------------------MEASUREMENT-----------------------------//
        #endregion
        #region DELETE
        public static void DeleteObserver(int id)
        {
            string stmt = "DELETE FROM observer WHERE id = @id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteScalar();
                }
            }
        }
        #endregion
    }
}



