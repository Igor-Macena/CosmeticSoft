/*
use master
go
DROP DATABASE bdcosmeticsoft
go
*/
CREATE DATABASE bdcosmeticsoft 
go 
USE bdcosmeticsoft 
go
CREATE TABLE tbcliente (
	codcli int PRIMARY KEY IDENTITY NOT NULL,
	nome varchar(60) NOT NULL,
	cpf char(11) NOT NULL,
	fone char(14),
	cel char(14) -- NEW
)
go
CREATE TABLE tbproduto (
	codprod int PRIMARY KEY IDENTITY NOT NULL,
	nome varchar(80) NOT NULL,
	preco MONEY NOT NULL,
	qtd int NOT NULL,
	custo money NOT NULL,
	validade date -- Pode ser nulo
)
-- select * from tbvenda -- comando para consultar tabela venda
go
CREATE TABLE tbrevendedor (
	codrevende int PRIMARY KEY IDENTITY NOT NULL,
	nome varchar(60) NOT NULL,
	senha varchar(50) NOT NULL,
	dicasenha varchar(60), -- Dica da senha tipo - senha: 08092015 dica: "data de nascimento do meu cachorro favorito"
	pergunta varchar(100) NOT NULL, -- vai ser usada no "esqueceu a senha?" tipo - pergunta: qual � o nome do seu animal favorito
	resposta varchar(80) NOT NULL, -- resposta: Lobo-guar�
	dicares varchar(80) --� uma dica opicional assim como a outra por exemplo a dica da resposta acima seria "� da fam�lia Can�deos"
)
go
CREATE TABLE tbvenda(
codvenda INT PRIMARY KEY IDENTITY NOT NULL,
datavenda DATE NOT NULL,
codcli int FOREIGN KEY REFERENCES tbcliente, 
codprod int FOREIGN KEY REFERENCES tbproduto, 
estatus varchar(15) not null,	-- (vendido) e (n�o vendido)
total money not null, --  armazena o valor total da venda 
lucro money not null -- armazena o lucro da venda
)
go

-- <><><><><><> -- SELECT tbvenda.codvenda, tbcliente.nome AS 'NOME DO CLIENTE', tbvenda.datavenda AS 'DATA', tbvenda.estatus AS 'STATUS DA VENDA', tbvenda.total AS 'VALOR DA VENDA' FROM tbvenda JOIN tbcliente ON tbvenda.codcli = tbcliente.codcli
/*
create procedure CONSULTA_VENDA
as
select tbcliente.nome, tbproduto.nome as Produto, tbproduto.preco as Valor from tbcliente, tbvenda left join tbproduto on tbvenda.codcli = tbproduto.codprod 
select datavenda, tbcliente.nome, datavenda from tbcliente
join tbvenda on tbcliente.codcli = tbvenda.codcli 
*/
--CADASTRANDO PRODUTOS
INSERT INTO tbproduto (nome, preco, qtd, custo,validade) VALUES ('Perfume', 80, 18, 60, '2020/08/05')
INSERT INTO tbproduto (nome, preco, qtd, custo,validade) VALUES ('Desodorante', 22, 28, 16, '2021/09/12')
INSERT INTO tbproduto (nome, preco, qtd, custo,validade) VALUES ('Sabonete', 29, 31, 19, '2022/07/07')

