SELECT * FROM Images;

DBCC CHECKIDENT (Images, RESEED, 0);

DELETE FROM Images;

INSERT INTO Images(Name, Annotation, UploadTime, Uploader, landmarks_LandmarkId)
		VALUES('birdlibrary.jpg',	'Bird Library',		'05/01/2014',	'admin',	1),
			  ('church.jpg',		'Hendricks Chapel',	'05/01/2014',	'admin',	2),
			  ('church2.jpg',		'Hendricks Chapel',	'05/01/2014',	'admin',	2),
			  ('dome1.jpg',			'Carrier Dome',		'05/01/2014',	'admin',	3),
			  ('dome2.jpg',			'Carrier Dome',		'05/01/2014',	'admin',	3),
			  ('dome3.jpg',			'Carrier Dome',		'05/01/2014',	'admin',	3),
			  ('music.jpg',			'Crouse College of Fine Arts',	'05/01/2014',	'admin',	4),
			  ('music2.jpg',		'Crouse College of Fine Arts',	'05/01/2014',	'admin',	4),
			  ('music3.jpg',		'Crouse College of Fine Arts',	'05/01/2014',	'admin',	4),
			  ('language.jpg',		'Hall of Language',	'05/01/2014',	'admin',	5);

SELECT * FROM CityImages;

DELETE FROM CityImages;

DBCC CHECKIDENT (CityImages, RESEED, 0);


INSERT INTO CityImages(Name, Annotation, UploadTime, Uploader)
		VALUES('city1.jpg',			'City picture 1',	'05/01/2014',	'admin'),
			  ('city2.jpg',			'City picture 2',	'05/01/2014',	'admin'),
			  ('city3.jpg',			'City picture 3',	'05/01/2014',	'admin'),
			  ('city4.jpg',			'City picture 4',	'05/01/2014',	'admin'),
			  ('city_art.jpg',		'city picture 5',	'05/01/2014',	'admin');