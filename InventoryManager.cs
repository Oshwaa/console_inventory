
//CRUD functions
public class InventoryManager
{
    static readonly List<Product> inventory = new List<Product>();
    static int nextId = 1;

    // Add -----------------------------------------------------------
    public static Product AddProduct()
    {
        try
        {
            string name = PromptString("\nEnter Product Name: ", "Product name cannot be empty.");
            int quantity = PromptInt("Enter Quantity: ", "Please enter a valid positive number for quantity.", minValue: 1);
            decimal price = PromptDecimal("Enter Price: ", "Please enter a valid non-negative number for price.", minValue: 0);

            var product = new Product
            {
                Id = nextId++,
                Name = name,
                Quantity = quantity,
                Price = price
            };

            inventory.Add(product);
            return product;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while adding the product.", ex);
        }
    }

    // View -----------------------------------------------------------
    public static List<Product> ViewInventory()
    {
        return inventory;
    }
    //SUM => Inventory (assuming inventory.Price is per unit/item)
    public static decimal GetTotalValue()
    {
        return inventory.Sum(product => product.Price * product.Quantity);
    }

    // Update -----------------------------------------------------------
    public static Product? UpdateProduct(int id)
    {
        try
        {
            var product = inventory.Find(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            string name = PromptString($"Enter new Name (current: {product.Name}): ", "Name cannot be empty.", allowEmpty: true);
            int quantity = PromptInt($"Enter new Quantity (current: {product.Quantity}): ", "Please enter a valid positive number for quantity.", minValue: 0, allowEmpty: true);
            decimal price = PromptDecimal($"Enter new Price (current: {product.Price}): ", "Please enter a valid non-negative number for price.", minValue: 0, allowEmpty: true);

            product.Name = !string.IsNullOrEmpty(name) ? name : product.Name;
            product.Quantity = quantity > 0 ? quantity : product.Quantity;
            product.Price = price > 0 ? price : product.Price;

            return product;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while updating the product.", ex);
        }
    }

    // Delete -----------------------------------------------------------
    public static Product? DeleteProduct(int id)
    {
        try
        {
            var product = inventory.Find(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            inventory.Remove(product);
            return product;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while deleting the product.", ex);
        }
    }

    // Helper functions ----------------------------------------------------
    // INPUT VALIDATIONS
    private static string PromptString(string message, string errorMessage, bool allowEmpty = false)
    {
        string? input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
            if (allowEmpty || !string.IsNullOrWhiteSpace(input)) break;
            Console.WriteLine(errorMessage);
        } while (true);

        return input!;
    }

    private static int PromptInt(string message, string errorMessage, int minValue = int.MinValue, bool allowEmpty = false)
    {
        int result;
        string? input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
            if (allowEmpty && string.IsNullOrWhiteSpace(input)) return -1;
            if (int.TryParse(input, out result) && result >= minValue) break;
            Console.WriteLine(errorMessage);
        } while (true);

        return result;
    }

    private static decimal PromptDecimal(string message, string errorMessage, decimal minValue = decimal.MinValue, bool allowEmpty = false)
    {
        decimal result;
        string? input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
            if (allowEmpty && string.IsNullOrWhiteSpace(input)) return -1;
            if (decimal.TryParse(input, out result) && result >= minValue) break;
            Console.WriteLine(errorMessage);
        } while (true);

        return result;
    }
}
