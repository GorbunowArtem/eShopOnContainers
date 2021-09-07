using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using System;
using AutoFixture;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using UnitTest.Ordering;
using Xunit;

public class OrderAggregateTest
{
    private readonly Fixture _fixture;

    public OrderAggregateTest()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Create_order_item_success()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = 5;

        //Act 
        var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

        //Assert
        Assert.NotNull(fakeOrderItem);
    }

    [Fact]
    public void Invalid_number_of_units()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = -1;

        //Act - Assert
        Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
    }

    [Fact]
    public void Invalid_total_of_order_item_lower_than_discount_applied()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = 1;

        //Act - Assert
        Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
    }

    [Fact]
    public void Invalid_discount_setting()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = 5;

        //Act 
        var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

        //Assert
        Assert.Throws<OrderingDomainException>(() => fakeOrderItem.SetNewDiscount(-1));
    }

    [Fact]
    public void Invalid_units_setting()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = 5;

        //Act 
        var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

        //Assert
        Assert.Throws<OrderingDomainException>(() => fakeOrderItem.AddUnits(-1));
    }

    [Fact]
    public void when_add_two_times_on_the_same_item_then_the_total_of_order_should_be_the_sum_of_the_two_items()
    {
        var address = new AddressBuilder().Build();
        var order = new OrderBuilder(address)
            .AddOne(1, "cup", 10.0m, 0, string.Empty)
            .AddOne(1, "cup", 10.0m, 0, string.Empty)
            .Build();

        Assert.Equal(20.0m, order.GetTotal());
    }

    [Fact]
    public void Add_new_Order_raises_new_event()
    {
        //Arrange
        var street = "fakeStreet";
        var city = "FakeCity";
        var state = "fakeState";
        var country = "fakeCountry";
        var zipcode = "FakeZipCode";
        var cardTypeId = 5;
        var cardNumber = "12";
        var cardSecurityNumber = "123";
        var cardHolderName = "FakeName";
        var cardExpiration = DateTime.Now.AddYears(1);

        //Act 
        var fakeOrder = new Order("1",
            "fakeName",
            new Address(street,
                city,
                state,
                country,
                zipcode),
            cardTypeId,
            cardNumber,
            cardSecurityNumber,
            cardHolderName,
            cardExpiration,
            "DISC-5",
            12m);

        //Assert
        Assert.Equal(fakeOrder.DomainEvents.Count, 1);
    }

    [Fact]
    public void Add_event_Order_explicitly_raises_new_event()
    {
        //Arrange   
        var fakeOrder = _fixture.Create<Order>();

        //Act 
        fakeOrder.AddDomainEvent(_fixture.Build<OrderStartedDomainEvent>()
            .With(os => os.Order, fakeOrder)
            .Create());
        
        //Assert
        Assert.Equal(2, fakeOrder.DomainEvents.Count);
    }

    [Fact]
    public void Remove_event_Order_explicitly()
    {
        //Arrange    
        var fakeOrder = _fixture.Create<Order>();
        var @fakeEvent = _fixture.Build<OrderStartedDomainEvent>()
            .With(o => o.Order, fakeOrder)
            .Create();

        //Act         
        fakeOrder.AddDomainEvent(@fakeEvent);
        fakeOrder.RemoveDomainEvent(@fakeEvent);
        
        //Assert
        Assert.Equal(1, fakeOrder.DomainEvents.Count);
    }
}