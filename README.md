# Accounts Manager
> This project serves as an assignment / assessment for a Solution Architect job position.
> The assessment consists of an API to be used for opening a new “current account” of already existing customers.

## Table of Contents
* [General Info](#general-information)
* [Technical Stack](#technical-stack)
* [Features](#features)
* [Setup](#setup)
* [Unit Test](#unit-test)


## General Information
- The solution consists of 4 projects: API layer, Service layer, Data layer and a project for Unit Testing.
- The database used is InMemory for the ease of testing.
- The design pattern implemented for Data layer is Repository Pattern with UnitOfWork Generic Repository.
- On startup, the database initial data is created. One test customer is added (Customer ID: "442be8d3-cf31-4ab6-a52e-9ecf97107ac1").
- No security layer has been implemented.
- Total hours spent: 10.


## Technical Stack
- .Net 6.0 framework
- Serilog for logging
- Swagger for API documentation
- AutoMapper for models mapping
- EF Core InMemory ORM
- XUnit for unit testing


## Features
The main controller AccountController provides the following endpoints:
- GetCustomers: returns the list of customers (with pagination)
- CreateCustomer: creates a new customer
- GetCustomerInfo: returns a customer's info by id along with balance, accounts, and account transactions
- CreateAccount: creates a customer's new account, and inital credit (if initial credit is greater than zero)
- AddAccountTransaction: adds a new transactions to a customer's specific account


## Setup
- Starting the startup project "AccountsManager.Services.Core.API" will launch Swagger UI where all endpoints can be accessed.
- A test customer will be created by default (Customer ID: "442be8d3-cf31-4ab6-a52e-9ecf97107ac1")
- Additional customers can be added using the Create Customer endpoint.


## Unit Tests
AccountTests class will create all needed Mock instances and initialize the InMemory database. 
To run unit tests open Test -> Test Explorer in VS and run the following tests:
- GetCustomerInfo_Success: asserts customer info endpoint success
- CreateCustomerAccount_Success: asserts customer account creation
- CreateCustomerAccount_InitialCreditAdded: asserts customer initial credit transaction creation after account creation
- CreateCustomerAccount_ZeroCredit_TransactionNotCreated: asserts no transactions created if initial credit is zero
- CreateCustomerAccount_InvalidCustomerId: asserts no account creation if customer not found
- AddAccountTransaction_Success: asserts new transaction creation
- AddAccountTransaction_ValidAccountBalance: asserts valid account balance after creating a new account transaction
- AddAccountTransaction_InvalidAccountNumber: asserts no transactions created if account number is invalid
- AddAccountTransaction_InvalidCustomerId: asserts no transaction creation if customer not found
- AddAccountTransaction_ZeroAmount: asserts no transaction creation if transaction amount is zero
- GetCustomerInfo_ValidBalance: asserts return customer info has a valid balance which is the sum of all accounts balances
- GetCustomers_Success: asserts get customers endpoint success
- CreateCustomer_Success: asserts new customer creation success

