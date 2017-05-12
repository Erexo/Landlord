using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class House
    {
        public int ID { get; protected set; }
        public string City { get; protected set; }
        public string Address { get; protected set; }
        public uint Rooms { get; protected set; }
        public uint Capacity { get; protected set; }
        public decimal Price { get; protected set; }
        public DateTime RentedAt { get; protected set; }
        public DateTime RentedUntil { get; protected set; }
        
        public User Owner { get; protected set; }

        [ForeignKey("Occupant")]
        public int? OccupantID { get; protected set; }
        public User Occupant { get; protected set; }

        public IList<Mark> Marks { get; private set; }

        protected House()
        {
        }

        public House(User owner, string city, string address, uint rooms, uint capacity, decimal price)
        {
            Owner = owner;
            SetCity(city);
            SetAddress(address);
            SetRooms(rooms);
            SetCapacity(capacity);
            SetPrice(price);
        }

        public void SetCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new Exception("Empty city parameter.");
            if (city.Length < 3 || city.Length > 64)
                throw new Exception("City parameter lenght must be between 3 and 64");
            if (City == city)
                return;

            City = city;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new Exception("Empty address parameter.");
            if (address.Length < 3 || address.Length > 64)
                throw new Exception("Address parameter lenght must be between 3 and 64");
            if (Address == address)
                return;

            Address = address;
        }

        public void SetRooms(uint rooms)
        {
            if (rooms < 1 || rooms > 32)
                throw new Exception("Rooms parameter must be between 1 and 32");
            if (Rooms == rooms)
                return;

            Rooms = rooms;
        }

        public void SetCapacity(uint capacity)
        {
            if (capacity < 1 || capacity > 256)
                throw new Exception("Capacity parameter must be between 1 and 256");
            if (Capacity == capacity)
                return;

            Capacity = capacity;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0 || price > 5000)
                throw new Exception("Price must be smaller than 5000 and higher than 0.");
            if (Price == price)
                return;

            Price = price;
        }

        public void addMark(User fromUser, Int16 value, string description)
        {
            Marks.Add(Mark.Create(this, fromUser, value, description));
        }
    }
}
