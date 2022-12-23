import csv
import os


game_assets_directory = "../ClashRoyale/ClashRoyale/GameAssets/csv_logic/"


def capitalize_words(filename: str):
    filename = filename.removesuffix(".csv")
    enum_name = ""
    for word in filename.split("_"):
        enum_name += word.capitalize()
    return enum_name


def get_enum_files():
    print("public enum Files")
    print("{")
    for i, filename in enumerate(os.listdir(game_assets_directory)):
        print(capitalize_words(filename) + " = " + str(i + 1) + ",")
    print("}")


def get_static_csv():
    print("static Csv()")
    print("{")
    for filename in os.listdir(game_assets_directory):
        filename = filename.removesuffix(".csv")
        capitalized_words = capitalize_words(filename)
        print(
            "DataTypes.Add(Files."
            + capitalized_words
            + ", typeof("
            + capitalized_words
            + "));"
        )
    print("}")

csv_datatype_to_cs_datatype = {
    "boolean": "bool",
    "booleanarray": "List<bool>",
    "intarray": "List<int>",
    "stringarray": "List<string>",
}
def make_csv_logic_class_file(filename, headers, datatypes):
    capitalized_words = capitalize_words(filename)
    new_file = f"output/{capitalized_words}.cs"
    if (os.path.exists(new_file)): os.remove(new_file)
    f = open(new_file, "a")
    f.write("using ClashRoyale.Files.CsvHelpers;\n")
    f.write("using ClashRoyale.Files.CsvReader;\n")
    f.write("\n")
    f.write("namespace ClashRoyale.Files.CsvLogic\n")
    f.write("{\n")
    f.write(f"public class {capitalized_words} : Data\n")
    f.write("{\n")
    f.write(f"\tpublic {capitalized_words}(Row row, DataTable datatable) : base(row, datatable)\n")
    f.write("\t{\n")
    f.write("\t\tLoadData(this, GetType(), row);\n")
    f.write("\t}\n")
    for header, datatype in zip(headers, datatypes):
        datatype = datatype.lower()
        if csv_datatype_to_cs_datatype.get(datatype):
            datatype = csv_datatype_to_cs_datatype.get(datatype)
        f.write(f"\tpublic {datatype} {header} {{ get; set; }}\n")
    f.write("\t}\n")
    f.write("}\n")

def make_csv_logic_class_files():
    for i, filename in enumerate(os.listdir(game_assets_directory)):
        with open(game_assets_directory + filename) as csv_file:
            csv_reader = csv.reader(csv_file, delimiter = ',')
            headers = next(csv_reader)
            datatypes = next(csv_reader)
            make_csv_logic_class_file(filename, headers, datatypes)
            
get_enum_files()
get_static_csv()
# make_csv_logic_class_files()