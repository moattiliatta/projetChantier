

CREATE DATABASE [ProjetChantier]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjetChantier', FILENAME = N'C:\Users\Moatt\Documents\GitHub\projetChantier\ProjetChantierLobby\ProjetChantier.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjetChantier_log', FILENAME = N'C:\Users\Moatt\Documents\GitHub\projetChantier\ProjetChantierLobby\ProjetChantier_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjetChantier].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

DROP TABLE if EXISTS "Employes";
DROP TABLE if EXISTS "Equipe";

CREATE TABLE "Equipe"(
"EquipeID" INTEGER PRIMARY KEY,
"NomDepartement" nvarchar(30) NOT NULL,



);

CREATE TABLE "Employes" (
	"EmployeID" INTEGER PRIMARY KEY,
	"Nom" nvarchar (20) NOT NULL ,
	"Prenom" nvarchar (10) NOT NULL ,
	"DateEmbauche" "datetime" NULL ,
	"Telephone" nvarchar (24) NULL ,
	"EquipeID" INTEGER FOREIGN KEY,


CONSTRAINT "FK_Employes_Equipe"	foreign key("EquipeID") references ("EquipeID")
);

