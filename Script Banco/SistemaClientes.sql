
-- Script Criacao do banco de dados
create database dbTeste
go

use dbTeste
go

-- Script Criacao Tabela Cliente

create table Cliente(
	ClienteId int identity(1,1) PRIMARY KEY,
	Cliente varchar(100) not null,
	TipoCliente varchar(100) not null,
	NomeContato varchar(100),
	TelefoneContato varchar(11),
	Cidade varchar(100) not null,
	Bairro varchar(100) not null,
	Logradouro varchar(100) not null,
	DataCadastro datetime not null default(getdate()),
	DataAtualizacao datetime
)

--Criando procs

CREATE PROC sp_listarTodosClientes
AS
    SELECT *
    FROM   cliente
go

CREATE PROC sp_listarClientePorId @Id INT
AS
    SELECT *
    FROM   cliente
    WHERE  clienteid = @Id

go 

CREATE PROC sp_cadastrarCliente @Nome            VARCHAR(100),
                                @TipoCliente     VARCHAR(100),
                                @NomeContato     VARCHAR(100),
                                @TelefoneContato VARCHAR(100),
                                @Cidade          VARCHAR(100),
                                @Bairro          VARCHAR(100),
                                @Logradouro      VARCHAR(100)
AS
    INSERT INTO cliente
                (cliente,
                 tipocliente,
                 nomecontato,
                 telefonecontato,
                 cidade,
                 bairro,
                 logradouro)
    VALUES     (@Nome,
                @TipoCliente,
                @NomeContato,
                @TelefoneContato,
                @Cidade,
                @Bairro,
                @Logradouro)

go 

CREATE PROC sp_atualizarCliente @Id              INT,
                                @Nome            VARCHAR(100),
                                @TipoCliente     VARCHAR(100),
                                @NomeContato     VARCHAR(100),
                                @TelefoneContato VARCHAR(100),
                                @Cidade          VARCHAR(100),
                                @Bairro          VARCHAR(100),
                                @Logradouro      VARCHAR(100)
AS
    UPDATE cliente
    SET    cliente = @Nome,
           tipocliente = @TipoCliente,
           nomecontato = @NomeContato,
           telefonecontato = @TelefoneContato,
           cidade = @Cidade,
           bairro = @Bairro,
           logradouro = @Logradouro,
           dataatualizacao = GETDATE()
    WHERE  clienteid = @Id

go

CREATE PROC sp_cadastrarVariosClientes @TotalCadastros int
AS
	declare @Contador int = 1

	WHILE @Contador <= @TotalCadastros  
	 BEGIN
		INSERT INTO cliente
                (cliente,
                 tipocliente,
                 nomecontato,
                 telefonecontato,
                 cidade,
                 bairro,
                 logradouro)
     VALUES     ('Cliente' + cast(@Contador as varchar(10)),
                'Tipo Cliente' + cast(@Contador as varchar(10)),
                'Nome Contato' + cast(@Contador as varchar(10)),
                'Telefone' + cast(@Contador as varchar(10)),
				'Cidade' + cast(@Contador as varchar(10)),
				'Bairro' + cast(@Contador as varchar(10)),
                'Logradouro' + cast(@Contador as varchar(10))
			  )
		set @Contador = @Contador + 1;
	 END;
go 

CREATE PROC sp_deletarClientePorId @Id INT
AS
    DELETE 
    FROM   cliente
    WHERE  clienteid = @Id
go 

--Criando 5 mil registros 

EXEC sp_cadastrarVariosClientes 5000


