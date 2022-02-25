function svg() {
  const eRestoranLogo = document.querySelectorAll('#eRestoranLogo path');
  const elementi = document.getElementById("eRestoranLogo").children;
  let delay = 0;
  for (let i = 0; i < 9; i++) {
    console.log(`slovo ${i} je ${eRestoranLogo[i].getTotalLength()}`);
    elementi[i + 1].style.strokeDasharray = eRestoranLogo[i].getTotalLength() + 'px';
    elementi[i + 1].style.strokeDashoffset = eRestoranLogo[i].getTotalLength() + 'px';
    elementi[i + 1].style.animation = `line-anim 2s ease forwards ${delay}s`;
    delay += 0.3;
  }
  setTimeout(() => {
    for (let i = 0; i < 9; i++) {
      elementi[i + 1].style.strokeDasharray = 0;
    }
  }, 2800);
}
