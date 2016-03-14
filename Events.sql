SELECT * FROM Events;

INSERT INTO Events(Name, time, Location, Description, EditTime, Editor)
			VALUES('SU Opera Op Shop Scenes: Setnor School of Music Ensemble Series',	'2014-05-04 14:00',	'Setnor Auditorium',	
			'For most events, free and accessible concert parking is available on campus in the Q-1 lot, located behind Crouse College. Additional parking is available in Irving Garage. Campus parking availability is subject to change, so please call 315-443-2191 for current information.',	
			'05/04/2014',	'admin'),
				('Biomedical & Chemical Engineering M.S. Thesis ',	'2014-05-05 21:30',	'369 Link Hall',
				'Theodore D. Williams, a candidate for the M.S. degree in chemical engineering, will defend his thesis. The title of his thesis is, "Geochemical Effects and Fate of Gold Nanoparticles (AuNP) in Saturated Soil Matrices." Radhakrishna Sureshkumar is the thesis advisor. All are invited to attend the presentation.',
				'05/04/2014',	'admin'),
				('Orange Orators: Toastmasters',	'2014-05-06 12:00',	'Bird Library, First Floor, Peter Graham Scholarly Commons',
				'Become the speaker and leader you want to be. Toastmasters offers a proven way to develop and hone your public speaking and leadership skills, while having fun doing so. The Orange Orators provides a safe place to build these skills in a mutually supportive and positive learning environment. Guests are always welcome!',
				'05/04/2014',	'admin'),
				('Oral Examination  ',	'2014-05-06 13:00',	'220 Huntington Hall',
				'You are invited to attend and participate in the oral doctoral examination of Maho Suzuki, candidate for the Ph.D. degree.
				Suzuki''s advisor is Professor Douglas Biklen. The dissertation examining committee will be composed of Professors Beth Ferri, Steven Taylor, Elisa Macedo Dekaney and Barbara Applebaum. Professor Robert Bifulco, Jr. will serve as chair of the defense.',
				'05/04/2014',	'admin'),
				('Biomedical & Chemical Engineering M.S.',	'2014-05-08 14:00',	'414 Bowne Hall',
				'Angela Nocera, a candidate for the M.S. degree in bioengineering, will defend her thesis. The title of her defense is, "Bio-based Polymeric Strategies for Drug Delivery." Rebecca Bader is the thesis advisor. All are invited to attend the presentation.',
				'05/04/2014',	'admin'),
				('Syracuse University Commencement',	'2014-05-11 9:30',	'Carrier Dome',
				'Commencement is for all graduates of Syracuse University and SUNY College of Environmental Science and Forestry. The ceremony begins with student and academic processions, followed by keynote speakers and the ceremonial conferring of degrees. ',
				'05/04/2014',	'admin');

DELETE FROM Events;

DBCC CHECKIDENT (Events, RESEED, 0);
