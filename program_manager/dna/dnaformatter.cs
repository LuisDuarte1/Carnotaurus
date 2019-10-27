using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace CarnotaurusV2{
    namespace Program_Manager{
        namespace NDNA{
            class DNAFormatter{

                private static readonly byte[] magic_number = new byte[] {0x12, 0x04, 0x14, 0x01};

                private static readonly byte[] inter_nucleotide_delimitator = {0x0, 0xf, 0x0, 0xf};
                private static readonly byte[] intra_nucleotide_delimitator = new byte[]{
                    0x69,0x69, 0x69, 0x69
                };
                private List<Nucleotide> genes = new List<Nucleotide>();
                public DNAFormatter(){

                }

                public void WriteToDNAFile(FileStream fs){
                    fs.Write(magic_number, 0, magic_number.Length); // write magic_number
                    fs.Write(BitConverter.GetBytes(genes.Count), 0, 4); //Every int32 value uses 4 bytes
                    int bytes_until_footer = 0;
                    List<byte[]> nucleotides_to_write = new List<byte[]>();
                    foreach (Nucleotide n in genes){
                        (byte[] type, byte[] gen_info, byte[] checksum, byte[] date) = n.GetNucleotideResult();
                        List<byte> bytes_nucleotide = new List<byte>();
                        bytes_nucleotide.AddRange(inter_nucleotide_delimitator.ToList()); //Add nucleotide delimitator
                        bytes_nucleotide.AddRange(type.ToList()); // Add type 
                        bytes_nucleotide.AddRange(intra_nucleotide_delimitator.ToList());
                        bytes_nucleotide.AddRange(gen_info.ToList()); // Add gen_info
                        bytes_nucleotide.AddRange(intra_nucleotide_delimitator.ToList());
                        bytes_nucleotide.AddRange(checksum.ToList()); // Add checksum
                        bytes_nucleotide.AddRange(intra_nucleotide_delimitator.ToList());
                        bytes_nucleotide.AddRange(date.ToList()); // Add date
                        bytes_until_footer += bytes_nucleotide.Count;

                        nucleotides_to_write.Add(bytes_nucleotide.ToArray());

                    }
                    fs.Write(BitConverter.GetBytes(bytes_until_footer), 0, 4);
                    foreach(byte[] n in nucleotides_to_write){
                        fs.Write(n, 0, n.Length);
                    }
                    fs.Flush();
                    fs.Close();
                }
                public void AddNucleotideToList(Nucleotide n){
                    n.CheckIfValid();
                    genes.Add(n);
                }

                private int GetNumberOfNucleotides(FileStream fs){
                    byte[] _int = new byte[4]; //Every int32 has 4 bytes
                    fs.Read(_int, magic_number.Length, _int.Length);
                    return BitConverter.ToInt32(_int);
                }

                private int GetBytesCountUntilFooter(FileStream fs){ 
                    //this is located in the magic_number length + number of nucleotides length
                    byte[] _int = new byte[4]; // int32 = 4 bytes
                    fs.Read(_int, magic_number.Length + 4, 4);
                    return BitConverter.ToInt32(_int);
                }
                private bool CheckMagicNumber(FileStream fs){
                    byte[] _magic_number = new byte[magic_number.Length]; //First 4 bytes checks if file is actually a valid dna file
                    fs.Read(_magic_number, 0, magic_number.Length);
                    if(magic_number.SequenceEqual(_magic_number)){
                        return true;
                    } else {
                        return false;
                    }
                }
                public bool CheckDNAFile(FileStream fs){ 
                    bool first_step = CheckMagicNumber(fs);
                    return first_step;

                }
            }
        }
    }
}