using System.Collections.Generic;
using System.Collections.Immutable;
using fNbt;
using NbtConverter;

//var path = Console.ReadLine();

var path = "C:\\My Stuff\\NBT Stuff\\caledonia\\agriculture\\horticulture\\florist5.nbt";
var _out = Console.ReadLine();

Converter.ConvertToMinecraftNbt(path, _out);