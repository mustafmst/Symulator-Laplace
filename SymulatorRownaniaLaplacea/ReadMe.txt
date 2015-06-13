Temat projektu: " Symulator do numerycznego rozwiązywania równania Laplace'a".

Opis:
- tworzymy szkielet obszaru, w ktorym równanie bedzie spełnione (obszar rysujemy za pomocą lini prostopadłych i równoległych);
- na granicach obszaru zadajemy potencjał brzegowy, ma on byc w granicach -100, 100, potencjał ten mamy zadawać poprzez kliknięcie myszką w dany bok obszaru, po kliknieciu ma się pokazać okienko, gdzie wpiszemy wartość i tak dla każdego boku;
- zakładamy wartość błędu e = 10^-6;
- na nasz obszar nakładamy siatke, odległość między punktami = 1 piksel;
- do obliczeń używamy metody różnic skończonych
iteracyjnie:
Vi,k^(n+1) = [Vi+1,k^(n) + Vi-1,k^(n) + Vi,k+1^(n) + Vi,k-1^(n)]/4
dopóki | Vi,k^(n+1) - Vi,k^(n) | < e

gdzie: i,k to kolejne numery węzłów, a V potencjał;
- wprowadzamy licznik, ktory będzie liczył ile pkt. spełnia juz powyższy warunek, kończymy obliczenia gdy licznik == liczbie pkt. obszaru;
- punktów brzegowych nie wolno nam zmieniać w procesie iteracyjnym, słuzą nam do rozpoczęcia procesu iteracyjnego;
- nasz obszar powinniśmy obłożyć tablicą dwuwymiarową, którą wypełnimy na początku zerami, pożniej wprowadzimy do niej wartości brzegowe i bedziemy wykonywac obliczenia potencjałów w pkt. wewnątrz naszego obszaru, dlatego podczas obliczeń musimy sprawdzać czy dany pkt. nalezy do obszaru lub czy jest pkt. granicznym. Punktów leżacych poza obszarem oczywiście nie bierzemy pod uwagę podczas obliczeń;
- w celu przyspieszenia zbieżności stosujemy metodę nadrelaksacji, wspólczynnik beta przyjmujemy 1,75;
- w efekcie program ma tworzyć mapkę potencjałów, znajdujemy Vmax i Vmin, tworzymy palete barw: robimy jakis podzial od Vmax do Vmin i sprawdzamy do jakiego przedzialu nalezy dany potencjał, np. Vmax = kolor czerwony, itd.;
- wypisać ile było iteracji, jak dlugo trwały obliczenia.

Chyba wszystko!!!!!!!!!!!!!!!!!!!!!!