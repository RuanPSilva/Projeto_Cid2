create database ProjetoCidade2;
use ProjetoCidade2;

create table usuario(
	idUser int primary key auto_increment,
	nomeUser varchar(30),
	email varchar(50),
	senha varchar(10)
);
create table produtos(
	idProd int primary key auto_increment,
	nomeProd varchar(50),
	descricao varchar(100),
	preco decimal (10,2),		
	quantidade int
);

