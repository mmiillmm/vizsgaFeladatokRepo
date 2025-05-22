using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace LaptopsCLI
{
    internal class Laptop
    {
        public Laptop(Category? category, string? cPU, string? manufacturer, string? model, string? oS, int price, string ram, string? screen, string? storage)
        {
            Category = category;
            CPU = cPU;
            Manufacturer = manufacturer;
            Model = model;
            OS = oS;
            Price = price;
            RAM = ram;
            Screen = screen;
            Storage = storage;
        }

        public Category ?Category { get; set; }
        public string ?CPU { get; set; }
        public string ?Manufacturer { get; set; }
        public string ?Model { get; set; }
        public string ?OS { get; set; }
        public int Price { get; set; }
        public string ?RAM { get; set; }
        public string ?Screen { get; set; }
        public string ?Storage { get; set; }


        public override string ToString()
        {
            return $"{Category} | {Manufacturer} ({Model} | {OS})";
        }

    }
}
