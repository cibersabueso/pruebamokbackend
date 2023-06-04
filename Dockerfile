# Imagen base para la API en .NET Core
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet

# Configuración de trabajo
WORKDIR /app

# Copiar los archivos de la primera API en el contenedor
COPY NorthwindAPI/NorthwindAPI.csproj ./NorthwindAPI/
COPY NorthwindAPI/*.cs ./NorthwindAPI/

# Restaurar las dependencias de la primera API
RUN dotnet restore NorthwindAPI/NorthwindAPI.csproj

# Publicar la primera API
RUN dotnet publish -c Release -o out NorthwindAPI/NorthwindAPI.csproj

# Imagen base para la segunda API en Python
FROM python:3.9 AS python

# Configuración de trabajo
WORKDIR /app

# Copiar los archivos de la segunda API en el contenedor
COPY SecondAPI/app.py ./SecondAPI/
COPY SecondAPI/requirements.txt ./SecondAPI/

# Instalar las dependencias de la segunda API
RUN pip install -r SecondAPI/requirements.txt

# Puerto expuesto para la segunda API
EXPOSE 5000

# Definir variables de entorno para RabbitMQ
ENV RABBITMQ_HOST=localhost
ENV RABBITMQ_QUEUE=my_queue

# Iniciar RabbitMQ en segundo plano
RUN rabbitmq-server -detached

# Iniciar la segunda API
CMD ["python", "SecondAPI/app.py"]
