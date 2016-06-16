using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern {
  class Program {
    static void Main(string[] args) {

      DepartmentStore departmentStore = new DepartmentStore();

      Shopper shopper1 = new Shopper("John");
      Shopper shopper2 = new Shopper("Mike");
      Shopper shopper3 = new Shopper("Joe");

      List<EventHandler<NewProductEventArgs>> items = new List<EventHandler<NewProductEventArgs>>() {
          shopper1.RecieveNewProductNotification,
          shopper2.RecieveNewProductNotification,
          shopper3.RecieveNewProductNotification,
      };

      foreach(var notificationFunc in items) {
        departmentStore.NewProductArrived += notificationFunc;
      }

      departmentStore.NewProduct("Furniture");
      departmentStore.NewProduct("Television");
      departmentStore.NewProduct("Monitor");

      Console.ReadLine();
    }
  }

  public class NewProductEventArgs : EventArgs {
    public string NewProduct { get; set; }
    public NewProductEventArgs(string productName) {
      this.NewProduct = productName;
    }
  }

  public class DepartmentStore {
    public event EventHandler<NewProductEventArgs> NewProductArrived;

    public void NewProduct(string productName) {
      if (NewProductArrived != null) {
        NewProductArrived(this, new NewProductEventArgs(productName));
      }
    }
  }

  public class Shopper {
    private string ShopperName { get; set; }
    public Shopper(string shopperName) {
      this.ShopperName = shopperName;
    }
    public void RecieveNewProductNotification(object sender, NewProductEventArgs e) {
      Console.WriteLine("Shopper {0} thanks for subscribing to our notifications. New product {1} has arrived.", this.ShopperName, e.NewProduct);
    }
  }
}
