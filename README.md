The NimblePayments SDK for NET
======================

The NimblePayments SDK for NET makes it easy to add payment services to your e-commerce or site access. It connects your site to the NimblePayments REST API directly.

## Requirements

* NET 4.5 or above

## Installation

### From source

1. Download or clone this repo. You will obtain a file called ".zip". It include the SDK and integration tests.
2. Unzip ".zip" and open NET.SDK.Nimblepayments solution.
2. Build solution and run test if necessary.
4. Now you are ready to include a reference of NimblePayments SDK in your project

## Configuration

The NimblePayments class includes some configuration parameters necessary to connect to Nimblepayments API. To create NimblePayment object, call to the constructor passing NimbleAuth object   

```csharp
var nimbleApi = new NimblePayments(new NimbleAuth
{
    ClientId = "YOUR CLIENT_ID",
    ClientSecrect = "YOUR CLIENT_SECRECT",
    Enviroment = NimbleEnviroment.Sandbox
});
```

If you have saved Application access token (tsec) and expiration time (expTime), you can include it in initialization

```csharp
var nimbleApi = new NimblePayments(new NimbleAuth
{
    ClientId = "YOUR CLIENT_ID",
    ClientSecrect = "YOUR CLIENT_SECRECT",
    ApplicationTsec = tsec,
    TsecExpireDateTime = expTime,
    
    Enviroment = NimbleEnviroment.Sandbox
});
```
> The parameter 'Enviroment' is set to define the environment and has two possible values: `NimbleEnviroment.Sandbox` or `NimbleEnviroment.Real`. 'Sandbox' is used in the demo environment to make tests and 'real' must be set to work in the real environment. 

# Payment

A "Payment" term refers to object that contains all the data needed in order to execute a payment. It is a class that must be informed with the following properties:

1. "Amount" refers to the amount that has to be paid. It must be inform in cents (multiplied by 100 the real amount and avoiding the decimals digits. )
2. "Currency" refers to the currency of the payment. It follows currency ISO 4217 code
3. "CustomerTransaction" refers to the merchant's identification of the sale. Example: The prestashop`s order id.
4. "UrlOk" refers to the callback URL to be redirected when the payment finishes successfully
5. "UrlKo" referes to the callback URL to be redirected when the payment finishes with error

## Register payment

Create a `NimblePayments` instance and send the payment via `GetPaymentUrlAsync` method in `Payments`.

```csharp
var operationResult = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
{
    UrlOk = "https://my-commerce.com/payments/success",
    UrlKo = "https://my-commerce.com/payments/error",
    Currency = "EUR",
    Amount = 1000,
    CustomerTransaction = "mY_INTERNAL_ID"
}, PaymentLanguageUi.Es);
```

The `PaymentLanguageUi` parameter is an enum to indicate language used in Payment user interface. At this moment Nimblepayment only support English and Spanish.
