import os


game_assets_directory = "../ClashRoyale/ClashRoyale/GameAssets/csv_logic"

print("public enum Files")
print("{")
for i, filename in enumerate(os.listdir(game_assets_directory)):
    filename = filename.removesuffix(".csv")
    enum_name = ""
    for word in filename.split("_"):
        enum_name += word.capitalize()
    print(enum_name + " = " + str(i + 1) + ",")
print("}")
print("static Csv()")
print("{")
for i, filename in enumerate(os.listdir(game_assets_directory)):
    filename = filename.removesuffix(".csv")
    enum_name = ""
    for word in filename.split("_"):
        enum_name += word.capitalize()
    print("DataTypes.Add(Files." + enum_name + ", typeof(" + enum_name + "));")
print("}")