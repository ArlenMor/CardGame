# CardGame
 
Мой первый опыт мобильной разработки.

Основная идея: игрок должен пройти какое-то “приключение”

“Приключение” зависит от выбранной колоды. Например, колода о спуске в подземелье, о постройке дома, о шпионской деятельности и так далее. В этом плане игра достаточно легко меняет сеттинг.
Желательно чтобы игра была реиграбельна и игровые сессии не занимали много времени, так как играться она будет на смартфоне. А смартфон в свою очередь - это небольшие сессии где угодно. В автобусе, в метро, в ожидании чего-то, дома на кровати и т.д.. Исходя из этого принято решение не развивать полноценную сюжетную линию (она может быть, но очень не большая) и сконцентрировать свое внимание на реиграбельности и механиках (ввиду выбранной игровой платформы).

Про опыт: игрок должен почувствовать себя героем того “приключение”, что он проходит. То есть если выбрана колода “подземелье”, то игрок должен ассоциировать себя с героем, должен испытывать постоянную опасность, страх смерти и потерю всего, что он нафармил (по типу Pixel Dungeon). Если игрок играет в приключение “Строительство”, то там игрок будет испытывать постоянное чувство ответственности за жителей, которые будут поселяться в его дом. Чувствовать дедлайны, сражаться с совестью (взять взятку или отсрочить зарплату ради удачного вложения) и так далее. Игрок должен находиться в постоянном “небольшом” дискомфорте, потому что людям нравится решать проблемы.

Основная цель: игрок, во время прохождения “приключения” будет иметь шкалу прогресса этого самого “приключения”. Она будет у всех колод. Задача игрока заполнить эту полоску. 
У каждой колоды будут свои уникальные характеристики для гг. Например, если вы играете в “подземелье” то у вас будут “здоровье”, “Энергия”, “Загруженность инвентаря”, “система ранений” и т.п. вещи. Если вы играете в “Строительство”, то там будут “Деньги”, “Время”, “Вместимость склада для техники”, “Довольство покупателей” и т.п.. 

Игра будет выдавать игроку определенную колоду карт с определенными событиями, пассивными эффектами, бафами и дебафами и т.п., а игрок будет выбирать, брать карту или нет. Та или иная карта будет влиять на характеристики персоонажа и уровень прохождения "приключения".

Девлог:
1. Загрузка характиристик карт из json файла и создание карты с определенными данными при помощи префаба
2. Добавлена система частей колод. Сейчас есть 3 части колоды по силе. В начале игры будут разыграны самые слабые карты, потом средние, потом самые сильные
3. Добавлена возможность перемешать карты в колоде
4. Добавлена отрисовка карт на экран, свайпы карт влево (отказаться от условия карты) и вправо (согласиться с условиями карты)
5. Добавлено стартовое меню и возможность выйти из игры
6. Добавленны новые шрифты
7. Добавлен экран загрузки
8. Переработан свайп карт
9. Добавлена анимация переворота карты
10. Добавлен UI
11. Добавлена логика UI
12. Добавлена смерть и конец игры
13. Переделана система загрузки из Json
14. Добавлена система вероятности (%) на успешное применение способности карты
15. Добавлен текст к количеству жизни и энергии
16. Добавлена анимация к уменьшению жизни и энергии
17. Добавлен инвентарь для сохраненных предметов (карт)
18. Добавлена возможность добавлять карты в инвентарь
19. Разработана система боя
20. Добавлены карты врагов
21. Добавлена возможность убивать врагов
22. В очередной раз обнавлена загрузка данных из json файла
23. Добавлена прокрутка в инвентарь
24. Придуманы основные стадии игры, их сложность
25. Начата разработка концепции минибоссов
26. Добавлена механника удара игрока враждебной картой
27. Добавлена анимация удара по карте
28. Переработана система колоды. Добавлены типы карт.
29. Добавлена анимация удара картой по игроку
30. Полностью переработана внутренняя архитектура игры (зеленые блоки - уже переработанные и добавленные в проект)
![architecture](https://user-images.githubusercontent.com/42170867/121810383-e1284d00-cc8a-11eb-9816-78e1442d803f.png)
31. Полность переработана архитектура врага и его скрипты
32. Полность переработана архитектура UI и его скрипты
33. Переработана концепция инвентаря. Теперь он разделен на блоки. 
34. Реализованы все скрипты инвентаря
35. Реализована карта типа Item
36. Реализовано добавление предмента в инвентарь
37. Реализована возможность использования предмета из инвентаря


Версия 1.0! (уже устарела (сильно устарела), скоро будет новая гифка)

![2021-05-02_22_25_12](https://user-images.githubusercontent.com/42170867/116818488-ce076500-ab95-11eb-9ca0-58cf82161707.gif)

Версия 1.2 (старая, скоро будет новая)

![20210702_174132](https://user-images.githubusercontent.com/42170867/124262956-e0355d80-db5c-11eb-8f39-cfd95c129956.gif)

Версия 1.3
Добавлен инвентарь, карты предметов, возможность использования предметов, система эффектов и система боя

![20210704_103504](https://user-images.githubusercontent.com/42170867/124372230-8c895800-dcb3-11eb-87b4-ca52071e9ee1.gif)
 

