# FiapTechChallenge
Espaço destinado para tech challenges referente ao curso de pós-graduação.

## 🤝 Colaboradores
* Hugo Souza - rm351477 
* Lucas Silva - rm351454

## 💻 FASE 4 - MICROSERVIÇOS E SERVERLESS
Essa fase do projeto contém, implementações: 
* Entity Framework Core
* Qualidade De Software
* Orquestração de containers com Kubernetes
* Clean Architecture 

# Documentação 📜
Ecommerce Tech Challenge - [Levantamento de requisitos e critério de aceite.pdf](https://github.com/hugorsouza/FiapTech/blob/master/Ecommerce%20Tech%20Challenge%20-%20Levantamento%20de%20requisitos%20e%20crit%C3%A9rio%20de%20aceite.pdf)

Ecommerce Tech Challenge - [Documentação técnica.pdf](https://github.com/hugorsouza/FiapTech/blob/master/Ecommerce%20Tech%20Challenge%20-%20Documenta%C3%A7%C3%A3o%20t%C3%A9cnica.pdf)

## 💻 Pré-requisitos

Para executar o projeto será necessário:

* Instalar o SDK .NET 7
* Ter uma instância do SQL Sever local
* Executar o script FiapTechChallenge/Scripts/Criar database.sql
* Definir a ConnectionString "ConnectionStrings:Ecommerce" do SQL Server no appsettings.json
* Definir a ConnectionString "BlobStorage:ConectionString" do Azure Blob Storage no appsettings.json
* Utilizar uma IDE como o VS 2022, Rider ou VSCode para executar o projeto

## 💻 Executando o projeto
Após realizar o setup inicial você poderá executar o projeto. Caso esteja executando em modo Development, uma carga de dados inicial será inserida no banco de dados para facilitar o acesso inicial a API.
Nessa carga inicial serão criados:
* Clientes e seus respectivos Usuários de acesso
* Funcionários e seus respectivos Usuários de acesso

Após executar o projeto, as rotas de documentação estarão disponíveis. Elas poderão ser consultadas em:
* Swagger: localhost:{porta}/swagger/index.html
* Redoc: localhost:{porta}/api-docs/index.html

## 💻 Realizando testes
Será possível efetuar os testes na API pelo Swagger. Para isso, recomendamos o uso dos usuários cadastrados na carga inicial dados de desenvolvimento.
Usuários disponíveis:
* Perfil Cliente: cliente@hotmail.com
* Perfil Funcionário (Admin): admin@hotmail.com
* Perfil Funcionário (Padrão): funcionario@hotmail.com
  
A senha padrão de todos os usuários criados durante essa carga inicial será "123456". Existirão outros usuários com dados aleatórios, eles foram criados utilizando a biblioteca Bogus.

Para se autenticar, utilize a rota /Autenticacao/login e utilize uma das credenciais disponibilizadas. Após obter o token de acesso, será necessário informar ele no botão Authorize para que assim seja possível acessar as rotas protegidas.

