In Azure I would create a resource group which acts as a container that holds related resources for an Azure solution. Such as Functions App, Web App, Static web app, SQL Database etc.

If a Queue is required I would opt for Method 2, but if not would choose Method 1 because I do like having whole API in one app and using swagger.

## Method 1 (Azure App Service)
- Create Azure App service that would contain the whole API. I would select a region where the application is going to be deployed I would decide this based on where the userbase would generally be.
- Deploy SPA using Azure Static Web App.

![Pasted image 20240901164728](https://github.com/user-attachments/assets/4ff46cbe-40d8-4e58-83ad-e84c2209922d)


## Method 2 (Function App)
- I would create a function app that would contain all functions and could share configurations between them. 
- Each endpoint could map to their own function. Each function would be configured to be triggered when  a http endpoint is hit. 
- I generally like being in a IDE so would create class library for my Azure functions and publish from within Visual Studio.
- Not in the diagram, but Azure Service bus could be used in-between static web app and function app. So if the function is down or busy requests could be stored in a queue and be processed when consumers are available. In this case the Azure function trigger would be when an item is placed into the queue.

 ![Pasted image 20240901165549](https://github.com/user-attachments/assets/6c378ed2-bbaf-4de8-9d01-aed5f9229154)


## Method 3 (API Management)
**I wouldn't suggest it for this application with the information I'm given.** If there are multiple different services being developed it would be useful to use API Management:
- Cross cutting concerns: Authentication, rate limiting, logging could be done in one place.
- All client would speak to one endpoint and depending on the policy configuration the API gateway would guide the requested to the desired Azure App Service. Also client code would be simplified. 

![image](https://github.com/user-attachments/assets/a58ada0e-f68e-4df3-b33c-6fc18cabac39)
