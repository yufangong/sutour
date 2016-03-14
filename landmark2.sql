﻿
SELECT * FROM Landmarks;

DBCC CHECKIDENT (Landmarks, RESEED, 0);

DELETE FROM Landmarks;

INSERT INTO Landmarks(LandmarkName, EditTime, Editor, Description)
	VALUES('Bird Library',		'05/01/2014',	'admin',	'Bird Library houses materials in the humanities and social sciences. Here you will also find audiovisual resources, government publications, and maps.  It is also home to the Learning Commons, an open, active environment where you can get research assistance, meet with a group, or study on your own, the Special Collections Research Center, Syracuse University Archives, Library administrative offices, and Pages, the library cafe.'),
		  ('Hendricks Chapel',	'05/01/2014',	'admin',	'Hendricks Chapel is the diverse religious, spiritual, ethical and cultural heart of Syracuse University that connects people of all faiths and no faith through active engagement, mutual dialogue, reflective spirituality, responsible leadership and a rigorous commitment to social justice.'),
		  ('Carrier Dome',		'05/01/2014',	'admin',	'A variety of events have been hosted at the Dome, including SU football, basketball, lacrosse, track and field, soccer, field hockey; professional and high school athletic events; University commencement and various academic convocations; ice skating shows; concerts; community events; and an annual celebration honoring the life of Dr. Martin Luther King Jr. At the time construction was completed, was ranked 5th largest domed stadium in the United States and the first of its kind in the Northeast.'),
		  ('Crouse College of Fine Arts',	'05/01/2014',	'admin',	'Built through the generosity of John Crouse, a Syracuse wholesale grocer, banker trustee of SU from its founding until his death in 1889, Mr. Crouse selected the building site and oversaw its construction. Originally named John Crouse Memorial College for Women, it was intended as a memorial to Crouse''s late wife; the title was engraved above the main entrance. Unfortunately, Mr. Crouse died prior to the building''s completion. His son, D. Edgar Crouse, immediately completed the structure, furnishings and grounds work. The exterior featured relief carvings including musical instruments and an artist''s palette over the front entrance. Four decorative chimneys and a bell tower rose above a hip and gable slate roof.'),
		  ('Hall of Language',	'05/01/2014',	'admin',	'This was the first building built on campus. Prior to its construction classes were held in the Myers Block on E. Genesee and Montgomery Streets in downtown Syracuse. The building was primarily an H-shape with recesses in the front and rear walls on either side of the central section. The rear recesses were partially occupied by coal houses. The east and west towers were part of the original construction; the central tower was not added until 1886. The east and west towers held large water tanks capable, it was believed, of flooding the entire structure in the event of fire. The west tower also held a 600 pound bell. The building originally rose 3½ stories in the central section and 2½ stories in the wings and was topped by a slate-covered mansard roof. Molded metal cornices sported stone brackets and the exterior walls had a "pecked" finish. The building was the home of the College of Liberal Arts from its beginning, although other schools and departments have also occupied the edifice, including the Registrar and the Chancellor. A section of the eastern wing is said to have been used as a natural science museum.');

Update Landmarks
	SET Direction = 'Go north straight to Schine Student Center. Then turn east to bird south gate'
	WHERE LandmarkId = 1;
Update Landmarks
	SET Direction = 'Go west straight to chapel directly.'
	WHERE LandmarkId = 2;
Update Landmarks
	SET Direction = 'Go south straight to Carnegie Library. Then go west straight to dome after passing by Archibold North.'
	WHERE LandmarkId = 3;
Update Landmarks
	SET Direction = 'Go north straight then turn west after passing by The College of Arts and Sciences. Then go straight then turn north after passing four crossing to arrive JC colleage of Fine Arts.'
	WHERE LandmarkId = 4;
Update Landmarks
	SET Direction = 'Go north straight then turn west to language hall after passing by School of Information Studies.'
	WHERE LandmarkId = 5;
