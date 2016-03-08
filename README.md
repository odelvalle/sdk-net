NimblePayments SDK for .NET
======================
The NimblePayments SDK for .NET makes it easy to add payment services to your e-commerce. It connects your site to the NimblePayments API directly.

## Release notes

### 1.0
- First live release
- It includes the single payment service 

## Requirements

* NET 4.5 or above

## Installation
The SDK zip from the GitHub repository contains the NimblePayments SDK for .NET tool, including  all its dependencies. Follow the next steps to install it:

1. Download latest/desired release zip. You will obtain a file called "sdk-net-master.zip" which includes the SDK and the integration tests.
2. Unzip "sdk-net-master.zip" and open NET.SDK.NimblePayments solution
2. Build solution and run test if necessary
4. Now you are ready to include the NimblePayments SDK reference in your project.

## Working with the SDK
Once you have completed the Installation and configuration processes, you are ready to generate a payment.

### How to create a payment
To generate a Payment you will need to:

1. Create a `NimblePayments` instance using configuration `NimbleAuth` object
2. Create a `Payment`object 
2. Use the `Payments.GetPaymentUrlAsync` method to send the `Payment` object

#### How to create a NimbleAuth object
In order to create a `NimblePayments` instance, you need to call the constructor providing a `NimbleAuth` object with the following properties:

- `ClientId`. It refers to the API_Client _ID obtained when generating a payment gateway in the NimblePayments dashboard.
- `ClientSecret`. It refers to the Client_Secret obtained when generating a payment gateway in the NimblePayments dashboard.
- `Environment`. It refers to the environment in which to work. It has two possible values:
    - `NimbleEnvironment.Sandbox`. It is used in the demo environment to make tests.
    - `NimbleEnvironment.Real`. It is used to work in the real environment. 


The `NimblePayments` class includes some configuration parameters necessary to connect to Nimblepayments API. To create `NimblePayments` object, call to the constructor passing `NimbleAuth` object   

```csharp
var nimbleApi = new NimblePayments(new NimbleAuth
{
    ClientId = "API_CLIENT_ID",
    ClientSecret = "API_CLIENT_SECRET",
    Environment = NimbleEnvironment.Sandbox
});
```

#### How to create a Payment object
A "`Payment`" term refers to an object that contains all the data needed in order to execute a payment:

- `Amount`: it refers to the amount that has to be paid in cents avoiding the decimal digits. The real amount has to be multiplied by 100.
- `Currency`: it refers to the payment currency. It follows the currency ISO 4217 code
- `CustomerTransaction`: it refers to an internal merchant's sale identificator. 
- `Urlok`: it refers to the callback URL to be redirected when the payment finishes successfully.
- `Urlko`: it refers to the callback URL to be redirected when the payment finishes with an error.

```csharp
var payment = new Payment
{
    UrlOk = "https://my-commerce.com/payments/success",
    UrlKo = "https://my-commerce.com/payments/error",
    Currency = "EUR",
    Amount = 1000,
    CustomerTransaction = "mY_INTERNAL_ID"
};
```

## Example of a Payment generation

```csharp
var nimbleApi = new NimblePayments(new NimbleAuth
{
    ClientId = "API_CLIENT_ID",
    ClientSecret = "API_CLIENT_SECRET",
    Environment = NimbleEnvironment.Sandbox
});

var operationResult = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
{
    UrlOk = "https://my-commerce.com/payments/success",
    UrlKo = "https://my-commerce.com/payments/error",
    Currency = "EUR",
    Amount = 1000,
    CustomerTransaction = "mY_INTERNAL_ID"
}, PaymentLanguageUi.Es);
```
> The `PaymentLanguageUI` parameter is used to indicate the language used in the Payment user interface. NimblePayments only supports English (En) and Spanish (Es).
 
## Test
You can find some code examples using the NimblePayments SDK for payments generation In the NimblePayments Test folder.

## Documentation
Please see [Apiary](http://docs.nimblepublicapi.apiary.io/#) for up-to-date documentation.
