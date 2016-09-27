using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeneticTSP
{
    public struct City
    {
        private int m_id;
        private int m_x;
        private int m_y;

        public int ID { get { return m_id; } }
        public int X { get { return m_x; } }
        public int Y { get { return m_y; } }

        public City(int id, int x, int y)
        {
            if(x < 0 || y < 0)
            {
                throw new ArgumentException($"Invalid argument x {x} or y {y}");
            }
            m_id = id;
            m_x = x;
            m_y = y;
        }
    }

    class TSPMap
    {
        private int m_height;
        private int m_width;
        private IDictionary<int, City> m_cities;
        private double? m_best_route;

        public TSPMap(int height, int width, int num_cities)
        {
            if(height <= 0 || width <= 0 || num_cities <= 0)
            {
                throw new ArgumentException($"Wrong argument height {height} or width {width} or num cities {num_cities}");
            }
            m_height = height;
            m_width = width;
            m_cities = CreateCitiesInCircle(num_cities);
            m_best_route = null;
        }

        public IDictionary<int, City> CreateCitiesInCircle(int num_cities)
        {
            IDictionary<int, City> cities = new Dictionary<int, City>();
            const int margin = 50;
            float radius;
            if(m_width < m_height)
            {
                radius = m_width / 2 - margin;
            }
            else
            {
                radius = m_height / 2 - margin;
            }

            Point origin = new Point(m_width / 2, m_height / 2);

            double angle_rad = Math.PI * 2 / num_cities;
            double angle_total = 0;
            int city_id = 0;
            while(angle_total < Math.PI * 2)
            {
                int city_x = (int)(origin.X + Math.Cos(angle_total) * radius);
                int city_y = (int)(origin.Y + Math.Sin(angle_total) * radius);
                var city_id_str = city_id.ToString();
                City city = new City(city_id, city_x, city_y);
                angle_total += angle_rad;

                cities[city_id] = city;
                ++city_id;
            }
            return cities;
        }

        public int NumCities
        {
            get { return m_cities.Count; }
        }

        public IDictionary<int, City> Cities
        {
            get { return m_cities; }
        }

        public double? BestPossibleRoute
        {
            get
            {
                if(m_best_route != null)
                {
                    return m_best_route;
                }
                else
                {
                    m_best_route = 0;
                    City start = m_cities[0];
                    foreach(int city_id in Enumerable.Range(0, m_cities.Count).Skip(1))
                    {
                        City end = m_cities[city_id];
                        m_best_route += distanceBetweenCities(start, end);
                        start = end;
                    }
                    // add a little extra to account for possible differences in starting positions
                    m_best_route += 2;
                    return m_best_route;
                }
            }
        }

        public double CalculateTourLength(IGenome<int> genome)
        {
            double tour_length = 0;
            City start = m_cities[genome.Data[0]];
            foreach(int city_id in genome.Data.Skip(1))
            {
                City end = m_cities[city_id];
                tour_length += distanceBetweenCities(start, end);
                start = end;
            }
            return tour_length;
        }

        private double distanceBetweenCities(City one, City two)
        {
            int x_diff = Math.Abs(one.X - two.X);
            int y_diff = Math.Abs(one.Y - two.Y);
            return Math.Sqrt(x_diff * x_diff + y_diff * y_diff);
        }
    }
}
