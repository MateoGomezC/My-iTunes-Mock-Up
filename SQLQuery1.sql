-- Set relation of ArtistId
CREATE TABLE Songs (
	Id int NOT NULL PRIMARY KEY,
	Title varchar(100) NOT NULL,
	Price float(2) NOT NULL,
	AverageRating float(2),
	ArtistId int NOT NULL,
);


CREATE TABLE Artist (
	Id int NOT NULL PRIMARY KEY,
	FullName varchar(100) NOT NULL,
);


-- Break relation between Song and Artist
CREATE TABLE SongArtist (
	Id int NOT NULL PRIMARY KEY,
	SongId int NOT NULL,
	ArtistId int NOT NULL,
);

-- Set ralations of the table
CREATE TABLE Ratings (
	Id int NOT NULL PRIMARY KEY,
	UserId int NOT NULL,
	SongId int NOT NULL,
	Rate int NOT NULL,
);

-- set relations
CREATE TABLE Purchase (
	Id int NOT NULL PRIMARY KEY,
	UserId int NOT NULL,
	SongId int NOT NULL,
	Amount float(2) NOT NULL,
	TransactionDate date NOT NULL,
);

-- set relations
CREATE TABLE Refund (
	Id int NOT NULL PRIMARY KEY,
	UserId int NOT NULL,
	SongId int NOT NULL,
	Amount float(2) NOT NULL,
	TransactionDate date NOT NULL,
);

CREATE TABLE Users (
	Id int NOT NULL PRIMARY KEY,
	FullName varchar(100) NOT NULL,
	Wallet float(2),
);

-- set relations
CREATE TABLE UserSongs (
	Id int NOT NULL PRIMARY KEY,
	UserId int NOT NULL,
	SongId int NOT NULL
);