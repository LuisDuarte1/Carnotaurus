using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarnotaurusV2{
    namespace Program_Manager{
        namespace NDNA{
            public class Nucleotide{
                private static readonly SHA256 s = SHA256.Create();
                private static readonly Encoding utf8 = Encoding.UTF8;

                private static readonly Random r = new Random();

                private string type;
                private byte[] genetic_info;

                private byte[] checksum;
                private DateTime date_modified;

                public Nucleotide(byte[] _genetic_info, string _type){
                    type = _type;
                    genetic_info = _genetic_info;
                    date_modified = DateTime.Now;
                    checksum = CalculateChecksum();
                    CheckIfValid();
                    CheckIfValid();
                }

                public Nucleotide(byte[] _genetic_info, byte[] _checksum, DateTime _datemodified, string _type){
                    type = _type;
                    genetic_info = _genetic_info;
                    checksum = _checksum;
                    date_modified = _datemodified;
                    CheckIfValid();
                }

                public (byte[] type, byte[] gen_info, byte[] checksum, byte[] date) GetNucleotideResult(){
                    CheckIfValid();
                    return (Encoding.UTF8.GetBytes(type), genetic_info, checksum, 
                    BitConverter.GetBytes(date_modified.ToBinary()));
                }

                private byte[] CalculateChecksum(){
                    //byte[] string_bytes;
                    byte[] date_bytes = BitConverter.GetBytes(date_modified.ToBinary());
                    byte[] pre_checksum = new byte[genetic_info.Length + date_bytes.Length];
                    Array.Copy(genetic_info, 0, pre_checksum, 0, genetic_info.Length);
                    Array.Copy(date_bytes,0, pre_checksum, genetic_info.Length, date_bytes.Length);
                    //Array.Copy(string_bytes, 0, pre_checksum, date_bytes.Length, string_bytes.Length);
                    return s.ComputeHash(pre_checksum);
                }

                public void CheckIfValid(){
                    if(checksum.SequenceEqual(CalculateChecksum()) == false){
                        throw new InvalidGeneticInformationException();
                    }
                }

                public void ModifyNucleotide(byte[] modified_info){
                    CheckIfValid();
                    genetic_info = modified_info;
                    date_modified = DateTime.Now;
                    checksum = CalculateChecksum();
                    CheckIfValid();
                }
            }
        }
    }
}