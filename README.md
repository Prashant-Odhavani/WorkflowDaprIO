# Fraud Detection Workflow with Dapr, .NET 8, and Microservices

This project demonstrates a **Fraud Detection System** built using **Dapr Workflow**, **.NET 8**, and **Microservices** architecture. The system coordinates multiple services to detect fraudulent activities in transactions by utilizing workflows, machine learning analysis, pattern detection, and historical data checks.

## Table of Contents
- [Introduction](#introduction)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [How to Run](#how-to-run)
- [Microservices](#microservices)
- [Dapr Workflow](#dapr-workflow)

## Introduction
The **Fraud Detection System** uses Dapr's Workflow engine to coordinate several microservices. The system checks transactions for known fraud patterns, analyzes them with machine learning, and reviews the transaction history. Based on the results, a decision is made whether the transaction is fraudulent, and the user is notified.

## Architecture

The system consists of the following components:
1. **TransactionService**: Entry point for starting the fraud detection workflow.
2. **FraudPatternService**: Checks the transaction against known fraud patterns.
3. **MLAnalysisService**: Uses machine learning to analyze the transaction.
4. **AccountHistoryService**: Verifies the account's historical transaction data.
5. **NotificationService**: Notifies the user of the fraud detection results.
6. **FraudDetectionWorkflow**: Dapr Workflow that coordinates the services.


## Technologies Used
- **Dapr** (Distributed Application Runtime) for service invocation, workflows, and sidecar pattern.
- **.NET 8** for building the microservices.
- **Docker** for containerization.

## Prerequisites

- **.NET 8 SDK**: Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
- **Dapr CLI**: Install the Dapr CLI from [Dapr's official website](https://docs.dapr.io/getting-started/install-dapr-cli/).
- **Docker**: Ensure that Docker is installed and running.
  
  ```bash
  # Verify Docker is running
  docker --version


## How to Run
1. Clone the Repository
2. Install Dapr Components by command "dapr init"
3. Run Each Microservice with Dapr
   - You will need to start each microservice with its corresponding Dapr sidecar. Here are the commands for each microservice:
   - ```bash
      dapr run --app-id transactionservice --app-port 5000 --dapr-http-port 3500 --app-protocol https --resources-path D:\yourdirectory\Components -- dotnet run --launch-profile https

      dapr run --app-id fraudpatternservice --app-port 5001 --dapr-http-port 3501 --app-protocol https --resources-path D:\yourdirectory\Components -- dotnet run --launch-profile https

      dapr run --app-id mlanalysisservice --app-port 5002 --dapr-http-port 3502 --app-protocol https --resources-path D:\yourdirectory\Components -- dotnet run --launch-profile https

      dapr run --app-id accounthistoryservice --app-port 5003 --dapr-http-port 3503 --app-protocol https --resources-path D:\yourdirectory\Components -- dotnet run --launch-profile https

      dapr run --app-id notificationservice --app-port 5004 --dapr-http-port 3504 --app-protocol https --resources-path D:\yourdirectory\Components -- dotnet run --launch-profile https
4. Start the Workflow
      ``` base
    curl -X POST http://localhost:5000/api/transaction/start \
      -H "Content-Type: application/json" \
      -d '{"TransactionId": "TX123", "Amount": 1500, "AccountId": "ACC789"}'
      

## Microservices
1. TransactionService
The entry point to start the fraud detection workflow using Dapr.
Exposes the /api/transaction/start endpoint to accept transaction requests.
2. FraudPatternService
Checks the transaction against known fraud patterns.
Exposes the /api/check endpoint for fraud pattern checks.
3. MLAnalysisService
Uses machine learning models to analyze transactions for potential fraud.
Exposes the /api/analyze endpoint.
4. AccountHistoryService
Verifies the account's historical transaction data to detect unusual behavior.
Exposes the /api/check endpoint.
5. NotificationService
Receives fraud detection results and notifies the user.
Exposes the /api/notify endpoint to receive notifications from the workflow.

## Dapr Workflow
The FraudDetectionWorkflow coordinates the activities in the following order:

- Check Fraud Patterns (FraudPatternService)
- Run Machine Learning Analysis (MLAnalysisService)
- Check Account History (AccountHistoryService)
- Notify the User (NotificationService)
  
The workflow is managed by Dapr and orchestrates these services, ensuring resilience, retries, and fault tolerance.

