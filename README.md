## Задание

Напишите сервер статистики для многопользовательской игры-шутера. Матчи этой игры проходят на разных серверах, и задача сервера статистики — строить общую картину по результатам матчей со всех серверов.

Сервер должен представлять собой standalone-приложение, реализующее описанный ниже RESTfulAPI.

Общая схема работы такая: игровые сервера анонсируют себя advertise-запросами, затем присылают результаты каждого завершенного матча. Сервер статистики аккумулирует разную статистику по результатам матчей и отдает её по запросам (статистика по серверу, статистика по игроку, топ игроков и т.д.).

## API

API сервера статистики состоит из следующих методов:

/servers/&lt;endpoint&gt;/info PUT, GET

/servers/&lt;endpoint&gt;/matches/&lt;timestamp&gt; PUT, GET

/servers/info GET

/servers/&lt;endpoint&gt;/stats GET

/players/&lt;name&gt;/stats GET

/reports/recent-matches[/&lt;count&gt;] GET

/reports/best-players[/&lt;count&gt;] GET

/reports/popular-servers[/&lt;count&gt;] GET

Все данные передаются в формате JSON. Каждый метод проиллюстрирован примером корректного запроса и ответа.

Структура JSON-а будет ясна из примеров: можно считать, что все указанные в примере поля должны содержаться в корректном запросе и других полей там не будет. Типы полей также однозначно определяются по примерам. Если в примере значением поля является целое число, значит оно может быть только целым. Все строковые значения могут состоять из произвольных unicode-символов, если не сказано иное.

Если в описании метода API не указано тело запроса, значит оно должно быть пустым. Если не указано тело ответа, предполагается пустой ответ с кодом 200 OK.

### Прием данных от игровых серверов

#### PUT /servers/&lt;endpoint&gt;/info (advertise-запрос)

Запрос:

{

        &quot;name&quot;: &quot;] My P3rfect Server [&quot;,

        &quot;gameModes&quot;: [&quot;DM&quot;, &quot;TDM&quot;]

}

Здесь endpoint является уникальным идентификатором сервера: при получении нового advertise-запроса с тем же endpoint-ом информация перезаписывается. endpoint имеет вид &lt;ipv4-address&gt;-&lt;port&gt; или &lt;hostname&gt;-&lt;port&gt;​.

#### PUT /servers/&lt;endpoint&gt;/matches/&lt;timestamp&gt;

Здесь timestamp— строка вида 2017-01-22T15:17:00Z, задающая момент окончания матча в UTC.

**Сервер не должен как-либо привязываться к времени на хосте. Результаты матчей с**  **timestamp**** -ом из прошлого или из будущего должны корректно сохраняться. Запросы с одного сервера всегда приходят в порядке возрастания **** timestamp ****-ов, но, из-за возможного расхождения времени между серверами, глобальный порядок не гарантируется.**

**Решение может корректно обрабатывать запросы с меньшим**  **timestamp**** -ом, чем самый большой на данный момент, но это не является обязательным требованием.**

Запрос:

{

        &quot;map&quot;: &quot;DM-HelloWorld&quot;,

        &quot;gameMode&quot;: &quot;DM&quot;,

        &quot;fragLimit&quot;: 20,

        &quot;timeLimit&quot;: 20,

        &quot;timeElapsed&quot;: 12.345678,

        &quot;scoreboard&quot;: [

                {

                        &quot;name&quot;: &quot;Player1&quot;,

                        &quot;frags&quot;: 20,

                        &quot;kills&quot;: 21,

                        &quot;deaths&quot;: 3

                },

                {

                        &quot;name&quot;: &quot;Player2&quot;,

                        &quot;frags&quot;: 2,

                        &quot;kills&quot;: 2,

                        &quot;deaths&quot;: 21

                }

]

}

Здесь scoreboardсодержит отсортированную по игровым очкам таблицу результатов матча. Победителем матча всегда является первый игрок в scoreboard.

Результаты матчей от серверов, не приславших advertise-запрос, не должны сохраняться. Таким серверам нужно отвечать пустым ответом с кодом 400 BadRequest.

### Получение текущей информации об игровых серверах

#### GET /servers/&lt;endpoint&gt;/info

Ответ:

Этот метод должен вернуть последнюю версию информации, полученную PUT-запросом по этому адресу в том же формате.

Если сервер с таким endpointникогда не присылал advertise-запрос, нужно вернуть пустой ответ с кодом 404 NotFound.

#### GET /servers/info

Ответ:

[

        {

                &quot;endpoint&quot;: &quot;167.42.23.32-1337&quot;,

                &quot;info&quot;: {

        &quot;name&quot;: &quot;] My P3rfect Server [&quot;,

        &quot;gameModes&quot;: [&quot;DM&quot;, &quot;TDM&quot;]

}

        },

{

                &quot;endpoint&quot;: &quot;62.210.26.88-1337&quot;,

                &quot;info&quot;: {

        &quot;name&quot;: &quot;&gt;&gt; Sniper Heaven &lt;&lt;&quot;,

        &quot;gameModes&quot;: [&quot;DM&quot;]

}

        }

]

Ответ должен содержать последнюю версию информации о всех серверах, когда-либо присылавших advertise-запрос.

#### GET /servers/&lt;endpoint&gt;/matches/&lt;timestamp&gt;

Ответ:

Этот метод должен вернуть информацию о матче, полученную PUT-запросом по этому адресу в том же формате.

Если PUT-запроса по этому адресу не было, нужно вернуть пустой ответ с кодом 404 NotFound.

### Получение статистики

Для методов из этой категории (имеются в виду методы \*/stats и reports/\*) скорость ответа важнее его актуальности. **Считается допустимым, если в статистике не будут учтены результаты матчей, присланные за последнюю минуту**.

#### Код подсчета статистики должен быть написан с заделом на возможное расширение: возможно добавление новых полей в методах \*/stats и новых методов в категории reports/\*.

#### GET /servers/&lt;endpoint&gt;/stats

Ответ:

{

        &quot;totalMatchesPlayed&quot;: 100500,

        &quot;maximumMatchesPerDay&quot;: 33,

        &quot;averageMatchesPerDay&quot;: 24.456240,

        &quot;maximumPopulation&quot;: 32,

        &quot;averagePopulation&quot;: 20.450000,

        &quot;top5GameModes&quot;: [&quot;DM&quot;, &quot;TDM&quot;],

        &quot;top5Maps&quot;: [

                &quot;DM-HelloWorld&quot;,

                &quot;DM-1on1-Rose&quot;,

                &quot;DM-Kitchen&quot;,

                &quot;DM-Camper Paradise&quot;,

                &quot;DM-Appalachian Wonderland&quot;,

]

}

Списки top5GameModes и top5Maps должны быть упорядочены по убыванию популярности (чем чаще встречается режим игры или карта среди всех матчей, тем он/она популярнее).

maximumMatchesPerDay, averageMatchesPerDay — максимальное и среднее количества сыгранных матчей на сервере за один календарный день по UTC (c 00:00Z до 00:00Z следующего дня). **Матч, сыгранный на границе дней, относится к тому дню, где он закончился.**

Для расчета среднего используется количество дней от первого матча на этом сервере до последнего матча среди всех серверов.

maximumPopulation, averagePopulation — максимальное и среднее количества игроков, принявших участие в одном матче.

#### GET /players/&lt;name&gt;/stats

Здесь name — urlencoded имя игрока.

Ответ:

{

        &quot;totalMatchesPlayed&quot;: 100500,

        &quot;totalMatchesWon&quot;: 1000,

        &quot;favoriteServer&quot;: &quot;62.210.26.88-1337&quot;,

        &quot;uniqueServers&quot;: 2,

        &quot;favoriteGameMode&quot;: &quot;DM&quot;,

        &quot;averageScoreboardPercent&quot;: 76.145693,

        &quot;maximumMatchesPerDay&quot;: 33,

        &quot;averageMatchesPerDay&quot;: 24.456240,

        &quot;lastMatchPlayed&quot;: &quot;2017-01-22T15:11:12Z&quot;,

        &quot;killToDeathRatio&quot;: 3.124333

}

averageMatchesPerDay: для расчета среднего используется количество дней от первого матча этого игрока до последнего матча среди всех матчей.

averageScoreboardPercent считается так:

Для конкретного матча scoreboardPercent = playersBelowCurrent / (totalPlayers - 1) \* 100%​.

Пример 1, в таблице 4 игрока:

Player1 — 100%

Player2 — 66.666667%

Player3 — 33.333333%

Player4 — 0%

Пример 2, в таблице 3 игрока:

Player1 — 100%

Player2 — 50%

Player3 — 0%

**Если в матче один игрок, scoreboardPercent = 100%.**

averageScoreboardPercent — это средний scoreboardPercent данного игрока по всем сыгранным матчам.

favoriteServer — сервер, на котором игрок появлялся чаще всего.

uniqueServers — количество уникальных серверов, на которых появлялся игрок.

favoriteGameMode — режим игры, в матчах с которым чаще всего участвовал игрок.

**Имена игроков должны сравниваться без учета регистра.**

#### GET /reports/recent-matches[/&lt;count&gt;]

Параметр count в этом и следующих методах задает число записей, которые нужно включить в отчет. Если записей меньше, нужно включить в отчет все. **Параметр необязательный и по умолчанию считается равным 5. Также**  **count**  **не может превосходить 50. Если в запросе он больше 50, нужно считать его равным 50, а если равен 0 или меньше 0 — вернуть пустой массив** [] **.**

Ответ:

[

        {

                &quot;server&quot;: &quot;62.210.26.88-1337&quot;,

                &quot;timestamp&quot;: &quot;2017-01-22T15:11:12Z&quot;,

                &quot;results&quot;:        {

        &quot;map&quot;: &quot;DM-HelloWorld&quot;,

        &quot;gameMode&quot;: &quot;DM&quot;,

        &quot;fragLimit&quot;: 20,

        &quot;timeLimit&quot;: 20,

        &quot;timeElapsed&quot;: 12.345678,

        &quot;scoreboard&quot;: [

                {

                        &quot;name&quot;: &quot;Player1&quot;,

                        &quot;frags&quot;: 20,

                        &quot;kills&quot;: 21,

                        &quot;deaths&quot;: 3

                },

                {

                        &quot;name&quot;: &quot;Player2&quot;,

                        &quot;frags&quot;: 2,

                        &quot;kills&quot;: 2,

                        &quot;deaths&quot;: 21

                }

]

}

        },

        ...

]

Здесь timestamp — время окончания матча в UTC, указанное в URL при загрузке его результатов.

**Последние матчи должны отсчитываться от матча с самым большим**  **timestamp**** -ом, а не от текущего времени на хосте.**

#### GET /reports/best-players[/&lt;count&gt;]

Ответ:

[

        {

                &quot;name&quot;: &quot;Player1&quot;,

                &quot;killToDeathRatio&quot;: 3.124333

        },

        ...

]

Здесь игроки должны быть отсортированы по убыванию killToDeathRatio. **При этом нужно игнорировать игроков, сыгравших менее 10 матчей, а также игроков, которые ни разу не умирали.**

killToDeathRatio = totalKills / totalDeaths, где totalKills— сумма kills игрока по всем сыгранным матчам, totalDeaths— сумма deaths игрока по всем сыгранным матчам.

#### GET /reports/popular-servers[/&lt;count&gt;]

Ответ:

[

        {

                &quot;endpoint&quot;: &quot;62.210.26.88-1337&quot;,

                &quot;name&quot;: &quot;&gt;&gt; Sniper Heaven &lt;&lt;&quot;,

                &quot;averageMatchesPerDay&quot;: 24.456240

        },

        ...

]

Здесь сервера должны быть отсортированы по убыванию averageMatchesPerDay.

## Требования к решению

- Сервер должен реализовывать описанное выше API.
- Сервер должен поддерживать работу на произвольном [HTTP](https://msdn.microsoft.com/en-us/library/windows/desktop/aa364698(v=vs.85).aspx) [-префиксе](https://msdn.microsoft.com/en-us/library/windows/desktop/aa364698(v=vs.85).aspx) (задается при запуске).
- Сервер не должен терять данные при перезапуске. Если на PUT-запрос сервер вернул 200 OK, данные не должны быть потеряны даже в случае аварийного завершения приложения. Допустима потеря данных при внезапном выключении хоста.

**Для хранения данных можно использовать как собственное решение, так и готовую**  **embedded**** -базу данных.**

- Сервер не должен падать при получении некорректных запросов. На такие запросы нужно отправлять ответ с кодом, отличным от 200 OK. Допустимы любые коды вида 4xx и 5xx.
- Сервер должен быстро обслуживать все виды запросов.
- Сервер должен уметь логировать ошибки.
- Код должен быть покрыт тестами.
- Код должен быть написан аккуратно и с любовью к деталям.

### Оформление решения

- Все внешние модули, от которых зависит решение, должны содержаться в папке с решением. Также допускаются зависимости от nuget-модулей.
- Решение должно успешно собираться с помощью msbuild: nuget restore &amp;&amp; msbuild /p:Configuration=Release. В результате сборки папка SWW.GStats.Server/bin/Release внутри папки с решением должна содержать всё необходимое для работы приложения.
- Сервер должен запускаться из папки bin/Release следующим образом: SWW.GStats.Server.exe --prefix [http](about:blank) [://+:8080/](about:blank), где вместо [http](about:blank) [://+:8080/](about:blank) может быть любой [UrlPrefix](https://msdn.microsoft.com/en-us/library/windows/desktop/aa364698(v=vs.85).aspx).
- Можно использовать в качестве заготовки проект из репозитория на гитхабе: [https](https://github.com/superwinwin-sww/SWW.GStats) [://](https://github.com/superwinwin-sww/SWW.GStats) [github](https://github.com/superwinwin-sww/SWW.GStats) [.](https://github.com/superwinwin-sww/SWW.GStats) [com](https://github.com/superwinwin-sww/SWW.GStats) [/](https://github.com/superwinwin-sww/SWW.GStats) [superwinwin](https://github.com/superwinwin-sww/SWW.GStats) [-](https://github.com/superwinwin-sww/SWW.GStats) [sww](https://github.com/superwinwin-sww/SWW.GStats) [/](https://github.com/superwinwin-sww/SWW.GStats) [SWW](https://github.com/superwinwin-sww/SWW.GStats) [.](https://github.com/superwinwin-sww/SWW.GStats) [GStats](https://github.com/superwinwin-sww/SWW.GStats)

### Отправка решения

- Решение необходимо разместить в репозитории своего аккаунта на сайте   [https](https://github.com/DQKrait/Kontur.GameStats) [://](https://github.com/DQKrait/Kontur.GameStats) [github](https://github.com/DQKrait/Kontur.GameStats) [.](https://github.com/DQKrait/Kontur.GameStats) [com](https://github.com/DQKrait/Kontur.GameStats)и отправить ссылку на почту [sww](mailto:sww.cv@bk.ru) [.](mailto:sww.cv@bk.ru) [cv](mailto:sww.cv@bk.ru) [@](mailto:sww.cv@bk.ru) [bk](mailto:sww.cv@bk.ru) [.](mailto:sww.cv@bk.ru) [ru](mailto:sww.cv@bk.ru).
- До дедлайна можно присылать новые версии решения, оценена будет последняя.

## Оценка решений

Порядок проверки решения примерно такой, но может незначительно поменяться на практике:

1. Проверка требований к оформлению решения. При несоответствии дальнейшие этапы проверки не проводятся.
2. Оценка полноты реализации API.
3. Оценка полноты соответствия требованиям к решению.
4. Нагрузочное тестирование. Примерный объем нагрузки описан ниже.
5. Оценка качества кода (для решений, достойно прошедших оценку по критериям 1-4).

### Нагрузочное тестирование

В распоряжении вашего сервера будет:

- Xeon E5-2650 @ 2.30 GHz
- 8 GB памяти
- 80 GB диска

Примерные ориентиры по нагрузке в худшем случае:

- Среднее число игроков в одном матче: ~50
- Максимальное число игроков в одном матче: ~100
- Общее количество серверов: ~10 000
- Всего уникальных игроков: ~1 000 000
- Всего дней в истории: ~14
- Среднее число матчей в день на одном сервере: ~100
- Количество уникальных режимов игры вряд ли превысит 10.
- Длина названия режима игры почти всегда не превосходит 3-х символов.
- Длина имени сервера, игрока или карты почти всегда не превосходит 50 символов.
