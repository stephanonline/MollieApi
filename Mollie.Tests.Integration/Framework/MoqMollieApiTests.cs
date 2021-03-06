﻿using Mollie.Api.Client;
using Mollie.Api.Models.Customer;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mollie.Tests.Integration.Framework {
    public class MoqMollieApiTests : BaseMollieApiTestClass {
        [TestCase]
        public async Task MoqMollieClient() {
            var mollieClientMock = new Mock<MollieClient>(ApiTestKey);
            var paymentResponse = new PaymentResponse { Id = "dummy_payment", Links = new PaymentResponseLinks() { PaymentUrl = "http://localhost/mollietest" } };
            var customerResponse = new CustomerResponse { Id = "dummy_customer" };

            mollieClientMock.Setup(x => x.CreatePaymentAsync(It.IsAny<PaymentRequest>())).Returns(() => Task.FromResult(paymentResponse));
            mollieClientMock.Setup(x => x.GetPaymentAsync(It.IsAny<string>())).Returns(() => Task.FromResult(paymentResponse));
            mollieClientMock.Setup(x => x.CreateCustomerAsync(It.IsAny<CustomerRequest>())).Returns(() => Task.FromResult(customerResponse));
            mollieClientMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).Returns(() => Task.FromResult(customerResponse));

            Assert.AreEqual(paymentResponse, await mollieClientMock.Object.CreatePaymentAsync(null));
            Assert.AreEqual(paymentResponse, await mollieClientMock.Object.GetPaymentAsync(null));
            Assert.AreEqual(customerResponse, await mollieClientMock.Object.CreateCustomerAsync(null));
            Assert.AreEqual(customerResponse, await mollieClientMock.Object.GetCustomerAsync(null));
        }
    }
}
