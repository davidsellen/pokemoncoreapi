# DotNet Core Pokemon API

A Pokemon List API

## Getting Started

This is DotNet Web API example that get its pokemon data from a remote API and caches it using Redis in order to deliver a full working solution of distributed application using dotnet core 2.0.

### Prerequisites

You're going to need Redis, I'm using an Docker image myself and you can get one by running:

```
docker run -d --name myRedis -p 6379:6379 redis
```

### Installing

You can get your development env running by following these steps:

Get the code

```
git clone git@github.com:davidsellen/pokemoncoreapi.git
```

Restore dotnet packages

```
dotnet restore
```

Run the Application

```
dotnet run
```

Navigate to http://localhost:5000/api/pokemon to get a list of pokemons

Navigate to http://localhost:5000/api/pokemon/1 to get a single pokemon

## Deployment

using docker follow these steps:

```
docker build -t pokerlist .
````

```
docker run -t -p 8080:80 pokerlist
```

now browse to http://localhost:8080/api/pokemon/ and see the JSON output of:

```javascript
[{"id":1,"name":"bulbasaur"},{"id":2,"name":"ivysaur"},{"id":3,"name":"venusaur"},{"id":4,"name":"charmander"},{"id":5,"name":"charmeleon"},{"id":6,"name":"charizard"},{"id":7,"name":"squirtle"},{"id":8,"name":"wartortle"},{"id":9,"name":"blastoise"},{"id":10,"name":"caterpie"},{"id":11,"name":"metapod"},{"id":12,"name":"butterfree"},{"id":13,"name":"weedle"},{"id":14,"name":"kakuna"},{"id":15,"name":"beedrill"},{"id":16,"name":"pidgey"},{"id":17,"name":"pidgeotto"},{"id":18,"name":"pidgeot"},{"id":19,"name":"rattata"},{"id":20,"name":"raticate"}]
```

## Built With

* [Redis](https://redis.io) - For distributed caching
* [DotNetCore](https://dotnet.github.io) - Web server
* [Docker](https://www.docker.com) - Ship web server and caching server as containers

## Authors

* **David Santos** - *Initial work* - [DavidSantos](https://github.com/davidsellen)

See also the list of [contributors](https://github.com/davidsellen/pokemoncoreapi/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* This API cosumes its pokemons from https://pokeapi.co
* Used some ideas from @github/renatogroffe [net-core-2-exemplos](https://medium.com/@renato.groffe/net-core-2-0-nosql-exemplos-utilizando-mongodb-documentdb-e-redis-be5f5407ff13)
