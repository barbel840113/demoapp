## DEMO APP 

#### spend time aprox 2 hours didn't use google as I prefer to use template which I can customize.

This service has following architecture implemented.
##### Architecture -> Onion Architecture
		This service has been scaffoled by Onion template as we apply DRY principle especially.
##### Layers
    ###### Presentation Layer -> Api or Web this is layer which will present data to end user or another services.
    ###### Application Core or Core Layers -> Domains object, including the business logic (using CQRS)
    ###### Infrastructure Layer -> Db Context and Repositories which will be handle storing data. As well as any shared services

###### Included
    * Swagger
    * CQRS (MediatR)
    * Logger
    * Repository Pattern