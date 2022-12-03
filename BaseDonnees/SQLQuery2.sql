
/*
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

*/

DROP TABLE if EXISTS "Employes";
DROP TABLE if EXISTS "Equipe";
DROP TABLE if EXISTS "Ouvrage";
DROP TABLE if EXISTS "Projet";
DROP TABLE if EXISTS "Sous-Traitant";
DROP TABLE if EXISTS "Materiaux"
DROP TABLE if EXISTS "Realiser";
DROP TABLE if EXISTS "Fournir";

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
	"EquipeID" INTEGER,


CONSTRAINT "FK_Employes_Equipe"	foreign key("EquipeID") references "Equipe"("EquipeID")
);


CREATE TABLE "Projet"(
	"ProjetID" INTEGER PRIMARY KEY,
	"EquipeID" INTEGER,
	"NomProjet" nvarchar(30) NOT NULL,
	"Date_Debut_Projet" nvarchar(30) NOT NULL,
	"Date_Fin_Projet" nvarchar(30) NOT NULL,

CONSTRAINT "FK_Projet_Equipe" foreign key("EquipeID") references "Equipe"("EquipeID")



);

CREATE TABLE "Ouvrage"(
"OuvrageID" INTEGER PRIMARY KEY,
"NomOuvrage" nvarchar (30) NOT NULL,
"Description Ouvrage" nvarchar(100) NOT NULL,
"EquipeID" INTEGER,
"Date_Debut_Ouvrage" nvarchar(30) NOT NULL,
"Date_Fin_Ouvrage" nvarchar(30) NOT NULL,

CONSTRAINT "FK_Ouvrage_Equipe" foreign key("EquipeID") references "Equipe" ("EquipeID")
);

CREATE TABLE "Sous-Traitant"(
"SousTraitantID" INTEGER PRIMARY KEY,
"DomainSousTraitant" nvarchar(30) NOT NULL,
"OuvrageID" INTEGER,
"Date_Debut_SousTraitant" nvarchar(30) NOT NULL,
"Date_Fin_SousTraitant" nvarchar(30) NOT NULL,

CONSTRAINT "FK_Sous-Traitant_Ouvrage" foreign key("OuvrageID") references "Ouvrage" ("OuvrageID")
);

CREATE TABLE "Materiaux"(
"MateriauxID" INTEGER PRIMARY KEY,
"NomMateriaux" nvarchar (30) NOT NULL,
"DateReception" nvarchar (30) NOT NULL,
"OuvrageID" INTEGER,

CONSTRAINT "FK_Materiaux_Ouvrage" foreign key("OuvrageID") references "Ouvrage" ("OuvrageID")
);

CREATE TABLE "Realiser"(
"RealiserID" INTEGER PRIMARY KEY,
"EquipeID" INTEGER,
"OuvrageID" INTEGER,
"Observations"  nvarchar (100) NOT NULL,

CONSTRAINT "FK_Realiser_Ouvrage" foreign key("OuvrageID") references "Ouvrage" ("OuvrageID"),
CONSTRAINT "FK_Realiser_Equipe" foreign key("EquipeID") references "Equipe" ("EquipeID")

);

CREATE TABLE "Fournir"(
 "MateriauxID" INTEGER,
 "OuvrageID" INTEGER,
 "DateReception" nvarchar (30) NOT NULL,


CONSTRAINT "FK_Fournir_Materiaux" foreign key("MateriauxID") references "Materiaux" ("MateriauxID"),
CONSTRAINT "FK_Fournir_Ouvrage" foreign key("OuvrageID") references "Ouvrage" ("OuvrageID")


);