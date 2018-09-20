# MegaDiceProj1
### Por Jean Luca e Pedro de la Peña

## Manual de instalação e uso do projeto
### Requisitos:
-Unity Engine
-NPM
-nodejs
-SQL 

	Inicialmente, deve-se rodar os arquivos localizados na pasta SQL Scripts. O arquivo que gera a base de dados é o Player_Base.sql e o arquivo playerbase_testdata.sql é utilizado para preencher a base com dados fictícios. 
	
	Em seguida, deve-se instalar o nodejs e rodar o comando npm install na pasta Rest do projeto. Com isso serão baixadas várias dependências por padrão, contudo, não serão utilizadas todas elas para o projeto. Em seguida, deve-se alterar o arquivo database.js localizado dentro da mesma pasta e deve-se alterar o usuário e senha a fim de realizar a conexão com o MySQL. Com a conexão do SQL realizada, deve-se executar o comando npm run start.
	
	No Unity, a primeira cena a ser rodada é a de login e esta define a criação do "objeto mestre" do projeto, e muitos dos scripts e conexões dependem deste objeto. Após a inicialização do projeto, o usuário é livre para explorar as funcionalidades do projeto.  Nesta versão o usuário tem acesso ao console e consegue observar quando um request falhou por determinados motivos. Dentre as possiveis (e esperadas) falhas em login estão: membro banido, senha incorreta ou usuário inexistente (avisos no console). 
