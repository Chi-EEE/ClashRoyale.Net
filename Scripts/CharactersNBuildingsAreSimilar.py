# NOTE: This script is unnecessary as both the characters.csv and the buildings.csv are the exact same, though this script is helpful to keep around

import csv
import os


game_assets_directory = "../ClashRoyale/ClashRoyale/GameAssets/csv_logic/"
characters_file = game_assets_directory + "characters.csv"
buildings_file = game_assets_directory + "buildings.csv"
csv_datatype_to_cs_datatype = {
    "boolean": "bool",
    "booleanarray": "List<bool>",
    "intarray": "List<int>",
    "stringarray": "List<string>",
}


def capitalize_words(filename: str):
    filename = filename.removesuffix(".csv")
    enum_name = ""
    for word in filename.split("_"):
        enum_name += word.capitalize()
    return enum_name


def make_csv_logic_class_file(filename, super_class, headers, datatypes):
    capitalized_words = capitalize_words(filename)
    new_file = f"output/{capitalized_words}.cs"
    if os.path.exists(new_file):
        os.remove(new_file)
    f = open(new_file, "a")
    f.write("using ClashRoyale.Files.CsvHelpers;\n")
    f.write("using ClashRoyale.Files.CsvReader;\n")
    f.write("\n")
    f.write("namespace ClashRoyale.Files.CsvLogic\n")
    f.write("{\n")
    f.write(f"public class {capitalized_words} : {super_class}\n")
    f.write("{\n")
    f.write(
        f"\tpublic {capitalized_words}(Row row, DataTable datatable) : base(row, datatable)\n"
    )
    f.write("\t{\n")
    f.write("\t\tLoadData(this, GetType(), row);\n")
    f.write("\t}\n")
    for header, datatype in zip(headers, datatypes):
        datatype = datatype.lower()
        if csv_datatype_to_cs_datatype.get(datatype):
            datatype = csv_datatype_to_cs_datatype.get(datatype)
        f.write(f"\tpublic {datatype} {header} {{ get; set; }}\n")
    f.write("}\n")
    f.write("}\n")


def get_header_to_datatype(file):
    with open(file) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=",")
        headers = next(csv_reader)
        datatypes = next(csv_reader)
        return dict(zip(headers, datatypes))


character_headers_to_data_type = get_header_to_datatype(characters_file)
building_headers_to_data_type = get_header_to_datatype(buildings_file)

character_header_set = set(character_headers_to_data_type.keys())
building_header_set = set(building_headers_to_data_type.keys())

same_headers = character_header_set & building_header_set
same_datatypes = [character_headers_to_data_type.get(header) for header in same_headers]

make_csv_logic_class_file("Entity", "Data", same_headers, same_datatypes)

character_headers = character_header_set.difference(same_headers)
character_datatypes = [
    character_headers_to_data_type.get(header) for header in same_headers
]
make_csv_logic_class_file(
    "Characters", "Entity", character_headers, character_datatypes
)
building_headers = building_header_set.difference(same_headers)
building_datatypes = [
    building_headers_to_data_type.get(header) for header in same_headers
]
make_csv_logic_class_file("Buildings", "Entity", building_headers, building_datatypes)
