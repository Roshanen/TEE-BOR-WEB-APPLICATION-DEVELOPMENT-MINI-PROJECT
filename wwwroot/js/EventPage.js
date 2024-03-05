var data =
{
'content-details': `
Discover the art of turning your creativity into currency in the era of digital advancement!

Join us for #POPupARTxSnF - an exclusive collaborative session featuring digital artist Johannes Oppewal (@Bitpopart). Together, we will delve into the world of Bitcoin, exploring how your artistic skills can become a source of income in the digital realm.

Connect with like-minded artists and crypto enthusiasts as you embark on an afternoon of games and sightseeing in the city. Immerse yourselves in the culture and settings that have shaped Johannes on his artistic journey.

Details:

🎨 Time: 10:30 AM onwards
🎨 Location: ME GOODY Space
🎨 Price:
💙 Students (and Early Birds, until 11th Feb): 995 Baht
💙 Regular: 1290 Baht

Session limited to the first 20 sign-ups.
Snacks and tea will be provided.

Reservation by confirmed payment:

Payment to Kasikorn Bank
Acc Name: Buddy Up Co., Ltd
Saving Acc: 136-3-38775-2
Branch: Silom

Please dm us your payment slip for confirmation via Instagram DM (@megoodyth) or Line ID (@megoody)

For more information or to secure your spot, DM or contact our Line Official chat.

#megoodytravelexcursion #Bitcoin #Bitkub #art #exhibition #SnF #SeekandFound #money #event #bangkok #culture #inspiration #worldincolour #connect`,
'tag':['art', 'bitcoin', 'digital', 'crypto', 'culture', 'inspiration', 'worldincolour', 'connect'],
'attendees':['Alice', 'Bob', 'Charlie', 'David', 'Emma', 'Frank', 'Grace', 'Henry', 'Ivy', 'Jack', 'Kate', 'Liam', 'Mia', 'Noah', 'Olivia', 'Parker', 'Quinn', 'Ryan', 'Sophia', 'Tyler', 'Uma', 'Victor', 'Willow', 'Xander', 'Yara', 'Zoe'],
'host-name': 'Cin',
'time':`วันเสาร์ที่ 2 มีนาคม 2567 เวลา 15:00 ถึง วันเสาร์ที่ 2 มีนาคม 2567 เวลา 16:00 GMT+7
Every week on Saturday until December 28, 2024`,
'place':`Café Amazon - อเมซอน สาขาหาดจอมเทียน
Jomtiensaineung Rd · Amphoe Bang Lamung, Ch`,
"host-img":"https://secure-content.meetupstatic.com/images/classic-member/311160299/45x45.jpg",
"map" : "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3875.851556636512!2d100.76987847455831!3d13.72743559785821!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x311d664bb802c079%3A0x947a912a091b0339!2z4LiE4LiT4Liw4Lin4Li04Lio4Lin4LiB4Lij4Lij4Lih4Lio4Liy4Liq4LiV4Lij4LmMIOC4quC4luC4suC4muC4seC4meC5gOC4l-C4hOC5guC4meC5guC4peC4ouC4teC4nuC4o-C4sOC4iOC4reC4oeC5gOC4geC4peC5ieC4suC5gOC4iOC5ieC4suC4hOC4uOC4k-C4l-C4q-C4suC4o-C4peC4suC4lOC4geC4o-C4sOC4muC4seC4hw!5e0!3m2!1sth!2sth!4v1708920473731!5m2!1sth!2sth" ,
'attend' : false,
'event-name' :'POPupART x SnF, Arts Meets Bitcoin',
'short-time':'THU, FEB 29 · 6:00 PM GMT+7',
'short-details':'[AWS Security Meetup Group TH - Session #3]'
}

// document.getElementById('event-name').innerHTML = data['event-name'];

// document.getElementById('host-name').innerHTML = data['host-name'];

// document.getElementById('host-img').src = data['host-img'];

// document.getElementById('content-details').innerHTML = data['content-details'].replace(/\n/g, '<br>');

// document.getElementById('time').innerHTML = data['time'].replace(/\n/g, '<br>');

// document.getElementById('place').innerHTML = data['place'].replace(/\n/g, '<br>');

// document.getElementById('tag').innerHTML = data['tag'].map(tag => `<a href="" class="tag">${tag}</a>`).join('\n');

// document.getElementById('num-attendees').innerHTML += `(${data['attendees'].length})`; 

// document.getElementById('attendees-list').innerHTML += `
// <section class="attendee-profile">
// <img
// src="https://secure-content.meetupstatic.com/images/classic-member/311160299/45x45.jpg"
// alt=""
// />
// <div>${data['host-name']}</div>
// <div>Host</div>
// </section>`

// document.getElementById('attendees-list').innerHTML += data['attendees'].map(attendee => `
// <section class="attendee-profile">
// <img
// src="https://secure-content.meetupstatic.com/images/classic-member/311160299/45x45.jpg"
// alt=""
// />
// <div>${attendee}</div>
// <div>member</div>
// </section>`).join('\n');

// document.getElementById('map').src = data['map'];

// document.getElementById('short-time').innerHTML = data['short-time'];

// document.getElementById('short-details').innerHTML = data['short-details'];

// if(data['attend']){
//   document.getElementById('attend-btn').innerHTML = 'Unattend';
// }

// document.getElementById('attend-btn').addEventListener('click', function() {
//   if(document.getElementById('attend-btn').innerHTML == 'Attend'){
//     document.getElementById('attend-btn').innerHTML = 'Unattend';
//     alert('You have attended this event');
//   }
//   else{
//     document.getElementById('attend-btn').innerHTML = 'Attend';
//     alert('You have unattended this event');
//   }
// });

document.getElementById('share').addEventListener('click', function() {
  navigator.clipboard.writeText(window.location.href).then(alert('Link copied to clipboard'));
})

document.addEventListener('DOMContentLoaded', function() {
    var container = document.getElementById('attend-line');
    var fixedElement = document.getElementById('attend');
    var scrollThreshold = container.offsetTop-fixedElement.offsetTop;
  
    function handleScroll() {
      if (window.pageYOffset >= scrollThreshold) {
        fixedElement.style.position = 'static';
      } else {
        fixedElement.style.position = 'fixed';
      }
    }
  
    window.addEventListener('scroll', handleScroll);
  });
  