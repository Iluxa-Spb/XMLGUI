﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
    class CreateXML
    {
        public CreateXML()
        {
			//задаем путь к нашему рабочему файлу XML
			string fileName = "base.xml";

			//счетчик для номера композиции
			int id = 1;
			//Создание вложенными конструкторами.
			XDocument doc = new XDocument(
				new XElement("db",
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Ульянова"),
						new XAttribute("type_id", "1"),
						new XElement("type_name", "Фитнес"),
						new XElement("last_name_coach", "Петрова"),
						new XElement("start_date", "20.07.2020 21:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "10")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Радионова"),
						new XAttribute("type_id", "1"),
						new XElement("type_name", "Фитнес"),
						new XElement("last_name_coach", "Петрова"),
						new XElement("start_date", "20.07.2020 21:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "10")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Наумова"),
						new XAttribute("type_id", "1"),
						new XElement("type_name", "Фитнес"),
						new XElement("last_name_coach", "Петрова"),
						new XElement("start_date", "20.07.2020 21:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "10")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Вердионова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Вердионова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Вердионова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Гувернова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Фламенцева"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Доброумова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Иванова"),
						new XAttribute("type_id", "2"),
						new XElement("type_name", "Танцы"),
						new XElement("last_name_coach", "Фламеева"),
						new XElement("start_date", "20.07.2020 19:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "15")),
					new XElement("user_account",
						new XAttribute("id", id++),
						new XElement("last_name_user", "Кардионова"),
						new XAttribute("type_id", "1"),
						new XElement("type_name", "Фитнес"),
						new XElement("last_name_coach", "Петрова"),
						new XElement("start_date", "20.07.2020 21:00:00"),
						new XElement("num_minutes", "60"),
						new XElement("rate", "10"))));
			//сохраняем документ
			doc.Save(fileName);
		}
    }
