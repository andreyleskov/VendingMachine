namespace VendingMachine.Tests
{
    using NUnit.Framework;

    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;

    [TestFixture]
    public class ProductBusketTests
    {

        [Test]
        public void Cant_dispense_more_than_existing_products()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());

            busket.Dispence();

            Assert.Throws<BusketIsEmptyException>(() => busket.Dispence(), "Не было выброшено исключение при выдаче из пустого диспенсера");
        }

        [Test]
        public void Emptied_busket_has_null_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());
            busket.Dispence();
            Assert.IsNull(busket.Product,"После выдачи последнего продукта в диспенсере остался демонстрационный продукт");
        }

        [Test]
        public void Emptied_busket_has_zero_amount()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());
            busket.Dispence();
            Assert.AreEqual(0, busket.Amount, "После выдачи последнего продукта в диспенсере неверный показатель остатка");
        }

        [Test]
        public void Busket_dispense_added_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            Tea tea = new Tea();
            busket.Add(tea);
            var product = busket.Dispence();
            Assert.AreEqual(tea,product);
        }


        [Test]
        public void Busket_counts_added_product()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));
            busket.Add(new Tea());
            busket.Add(new Coffe());

            Assert.AreEqual(2, busket.Amount);
        }



        [Test]
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


        [Test]
        public void Cant_dispense_from_empty_busket()
        {
            var busket = new ProductBusket(1, new Money(Currency.Fake, 1));

            Assert.Throws<BusketIsEmptyException>(() => busket.Dispence(),"Не было выброшено исключение при выдаче из пустого диспенсера");
        }
    }
}
