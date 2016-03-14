var CaptionedImages = [
    "<div><div><img src='students/MohammedKoni.jpg' height='350' border=0 /></div><div>Chatting with Mohammed Koni</div></div>",
    "<div><div><img src='students/swdev3.jpg' height='350' border=0 /></div><div>SW Studio Project Demo</div></div>",
    "<div><div><img src='students/PriyaaNachimuthu6.jpg' height='350' border=0 /></div><div>Lunch with Microsoft Recruiter Priyaa</div></div>",
    "<div><div><img src='students/Grad13e.jpg' height='350' border=0 /></div><div>With Hao Zhang at grad reception</div></div>",
    "<div><div><img src='students/MerwynDSilva.jpg' height='350' border=0 /></div><div>Merwyn DSilva stops to say goodbye</div></div>",
    "<div><div><img src='students/HelpSession.jpg' height='350' border=0 /></div><div>Friday help session</div></div>",
    "<div><div><img src='students/OODExam.jpg' height='350' border=0 /></div><div>CSE687-OOD Midterm Exam</div></div>",
    "<div><div><img src='students/JingyiRen.jpg' height='350' border=0 /></div><div>Jingyi now at Amazon</div></div>",
    "<div><div><img src='students/AbhishekTummala.jpg' height='350' border=0 /></div><div>Meeting Abhishek's Mother</div></div>", 
    "<div><div><img src='students/Foundry2.jpg' height='350' border=0 /></div><div>Signing off on Project Qualification</div></div>",
    "<div><div><img src='students/MontyJain.jpg' height='350' border=0 /></div><div>Monty Jain at Microsoft</div></div>",
    "<div><div><img src='students/VickySingh2.jpg' height='350' border=0 /></div><div>Vicky Singh Former TA</div></div>", 
    "<div><div><img src='students/VijayRamakrishnan2.jpg' height='350' border=0 /></div><div>Vijay Ramakrishnan Former TA</div></div>"
];
var rand = 60 / CaptionedImages.length;
var currentDate = 0;
var imageNumber = 0;
function randomimage() {
    currentDate = new Date();
    image_number = currentDate.getSeconds();
    image_number = Math.floor(image_number / rand);
    return (CaptionedImages[image_number]);
}