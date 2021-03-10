###Todas as funcionalidades da API necessitam de autenticação.

- Para requisitar um token de acesso, acesse a seguinte rota:
https://apirodrigosiquinelli.azurewebsites.net/api/security/token

		username - admin
		password - admin	
		grant_type - password

- Será recebido um token com validade de 480 minutos.

####Para testar a API  (Utilizei somente o Postman para os testes).
- Insira o token que foi recebido na aba Auth (Bearer Token) ou Header (Authorization).

---------------

- Estes métodos não necessitam de nenhum parâmetro.

- - https://apirodrigosiquinelli.azurewebsites.net/api/Pessoa/BuscarPessoas

- - https://apirodrigosiquinelli.azurewebsites.net/api/Usuario/BuscarUsuarios

------------

- Para estes métodos é necessário passar nos params o "codPessoa", "nome" ou "id".

- - https://apirodrigosiquinelli.azurewebsites.net/api/Pessoa/BuscarPessoaPorID **(codPessoa)**

- - https://apirodrigosiquinelli.azurewebsites.net/api/Pessoa/BuscarPessoaPorNome ** (nome)**

- - https://apirodrigosiquinelli.azurewebsites.net/api/Usuario/BuscarUsuarioPorID **(id)**

-------

- Para estes métodos é necessário passar no body os dados de usuario ou pessoa
- *O usuario logado pode apenas alterar os proprios dados e se não tiver perfil admin, não pode alterar "perfil" nem "ativo" (deleção lógica).*

- - https://apirodrigosiquinelli.azurewebsites.net/api/Usuario/AlterarUsuario **(codUsuario,login,senha,perfil,ativo)**

- - https://apirodrigosiquinelli.azurewebsites.net/api/Pessoa/AlterarPessoa  **(codPessoa,nome,sobrenome,dataNascimento,sexo,ativo)**

-----------------

- Para estes métodos é necessário passar no body os dados de usuario ou pessoa

- - https://apirodrigosiquinelli.azurewebsites.net/api/Usuario/CadastrarUsuario (login,senha,perfil,ativo)

- - https://apirodrigosiquinelli.azurewebsites.net/api/Pessoa/CadastrarPessoa  (nome,sobrenome,dataNascimento,sexo,ativo)

---------------

####End