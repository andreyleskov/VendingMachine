using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendingMachine;
    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;

    [TestClass]
    public class ProductBusketTests
    {
        [TestMethod]
        public void Added_product_can_be_dispensed()
        {
            
        }

        //TODO: убрать try\catch
        [TestMethod]
        public void Cant_dispense_more_than_existing_products()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());

            busket.Dispence();

            try
            {
                busket.Dispence();
            }
            catch (BusketIsEmptyException)
            {
                return;
            }
            Assert.Fail("Не было выброшено исключение при выдаче из пустого диспенсера");
        }

        //TODO: разделить на два теста
        [TestMethod]
        public void Emptied_busket_has_valid_properies()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());
            busket.Dispence();
            Assert.IsNull(busket.Product,"После выдачи последнего продукта в диспенсере остался демонстрационный продукт");
            Assert.AreEqual(0, busket.Amount,"После выдачи последнего продукта в диспенсере неверный показатель остатка");
        }


        [TestMethod]
        public void Busket_dispense_added_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            Tea tea = new Tea();
            busket.Add(tea);
            var product = busket.Dispence();
            Assert.AreEqual(tea,product);
        }


        [TestMethod]
        public void Busket_counts_added_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());
            busket.Add(new Coffe());

            Assert.AreEqual(2, busket.Amount);
        }



        [TestMethod]
        public void Busket_displas_last_added_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            Tea product = new Tea();

            busket.Add(product);
            Assert.AreEqual(product, busket.Product);

            Coffe coffe = new Coffe();
            busket.Add(coffe);

            Assert.AreEqual(coffe, busket.Product);
        }


        //TODO: убрать try\catch
        [TestMethod]
        public void Cant_dispense_from_empty_busket()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));

            try
            {
                busket.Dispence();
            }
            catch (BusketIsEmptyException)
            {
                return;
            }
            Assert.Fail("Не было выброшено исключение при выдаче из пустого диспенсера");
        }
    }
}
