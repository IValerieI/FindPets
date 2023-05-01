# FindPets

How to start a project:

1. Start the PostgreSQL in the Docker

docker pull postgres

docker run --name postgres --restart=always -p 25432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=Passw0rd -e POSTGRES_DB=postgres -v postgresvolume:/var/lib/postgresql/data -d postgres

2. Go to https://ethereal.email/create and copy (from section SMTP configuration) Host, Username, Password to corresponding fields in method SendEmail 

3. Set FindPets.Api as Startup Project and run it.
