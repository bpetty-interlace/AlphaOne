# Tenant Management Portal - Web Services

This is the Tenant Management Portal's Service Layer.  It is implemented as a GraphQL API hosted through Azure Functions.

**TODO**: Build a Static Web App to host the TMP's web content.


## Initializing DB with Entity ORM
**NOTE** - Change the hard-coded creds in **connectionString**, in **Program.cs**.
```
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --output-dir Entity/Migrations
dotnet ef database update
```

## GraphQL

Run the Azure Function locally and open the GraphQL explorer
EX: http://localhost:7070/api/graphql

Hot Chocolate will host, out of the box, and instance of Banan Cake Pop.  Insaine names, I know.

First, create a new Document.
In BCP, open the Schema Definitions tab to see the full Graph Schema.
Swith back to the Operations tab to run Graph commands.

### Upserting Data
When you do not provide an id, Entity is configured to insert and auto-increment ids.
If you provide an id, then it will switch over to an update.
```graphql
mutation {
    addResource(resource: {
        jwt: "123456"
        url: "https://east-1.interlace.health"
        tenants: [
            {
                name: "Liberty Hospital"
                nameKey: "LIB"
            },
            {
                name: "North Kansas City Hospital"
                nameKey: "NKC"
            }
        ]
    })
    {
        id
    }
}
```

### Querying Data - Return All
To return all results, run a query without any parameters.
You can specify any property you want returned and it will filter it at the database level.
```graphql
query {
  resources {
    id
    jwt # TODO: We should never actually allow this to be returned
    url
    tenants {
        id
        name
        nameKey
    }
  }
}
```

### Querying Data - Filtering

You can filter on any Model property.
```graphql
query {
  resources(where: { jwt: { eq: "123456" } }) {
    id
    jwt
    url
  }
}
```

You can even filter on related properties.
```graphql
query {
  resources(where: { 
        tenants: { some: { nameKey: { eq: "NKC" } } }
    }) {
    id
    jwt
    url
    tenants {
        name
        nameKey
    }
  }
}
```

### Querying Data - Sort Order
This is an example of a simple sort.
You can combine Sorting, Filtering, and even Pagination.  It is all built into GraphQL.
```graphql
query {
  resources(order: [{id: DESC } ]) {
    id
    jwt
    url
  }
}
```
