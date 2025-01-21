using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Console Application Inventory");

        while (true)
        {
            DisplayInventory();
            GetTotalValue();
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    UpdateProduct();
                    break;
                case "3":
                    DeleteProduct();
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    static void DisplayInventory()
    {
        var inventory = InventoryManager.ViewInventory();

        Console.WriteLine("\n--- Inventory ---");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (var product in inventory)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price:C}");
            }
        }
    }
    static void GetTotalValue(){
        try{
            var totaValue = InventoryManager.GetTotalValue();
            Console.WriteLine($"Inventory total Value: {totaValue}");
        }catch(Exception e){
            Console.WriteLine($"Failed to fetch Value: {e.Message}");
        }
    }
    static void AddProduct()
    {
        try
        {
            var product = InventoryManager.AddProduct();
            Console.WriteLine($"Product added successfully! ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add product: {ex.Message}");
        }
    }

    static void UpdateProduct()
    {
        try
        {
            Console.Write("Enter the ID of the product to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var updatedProduct = InventoryManager.UpdateProduct(id);
                if (updatedProduct != null)
                {
                    Console.WriteLine($"Product updated successfully! ID: {updatedProduct.Id}, Name: {updatedProduct.Name}, Quantity: {updatedProduct.Quantity}, Price: {updatedProduct.Price:C}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid product ID.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to update product: {ex.Message}");
        }
    }

    static void DeleteProduct()
    {
        try
        {
            Console.Write("Enter the ID of the product to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var deletedProduct = InventoryManager.DeleteProduct(id);
                if (deletedProduct != null)
                {
                    Console.WriteLine($"Product deleted successfully! ID: {deletedProduct.Id}, Name: {deletedProduct.Name}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid product ID.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete product: {ex.Message}");
        }
    }
}
