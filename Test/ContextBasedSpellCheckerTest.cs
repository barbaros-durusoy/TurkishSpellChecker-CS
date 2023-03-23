﻿using Corpus;
using MorphologicalAnalysis;
using NGram;
using NUnit.Framework;
using SpellChecker;

namespace Test
{
    public class ContextBasedSpellCheckerTest
    {
        [Test]
        public void TestSpellCheck()
        {
            Sentence[] original =
            {
                new Sentence("bugünkü ortaöğretim sisteminin oluşumu Cumhuriyet döneminde gerçekleşmiştir"),
                new Sentence(
                    "bilinçsizce dağıtılan kredilerin sorun yaratmayacağını düşünmelerinin nedeni bankaların büyüklüğünden kaynaklanıyordu"),
                new Sentence(
                    "Yemen'de ekonomik durgunluk nedeniyle yeni bir insani kriz yaşanabileceği uyarısı yapıldı"),
                new Sentence("hafif ticari araç bariyerlere çarptı"),
                new Sentence("olayı akşam haberlerinde birinci haber olarak verdiler"),
                new Sentence("dünyada geçen hafta da iktidarları sallayan sosyal çalkantılarla dolu geçti"),
                new Sentence("sırtıkara adındaki canlı , bir balıktır"),
                new Sentence("siyah ayı , ayıgiller familyasına ait bir ayı türüdür"),
                new Sentence("yeni sezon başladı gibi"),
                new Sentence("alışveriş için markete gitti"),
                new Sentence("küçük bir yalıçapkını geçti"),
                new Sentence("meslek odaları birliği yeniden toplandı"),
                new Sentence("yeni yılın sonrasında vakalarda artış oldu"),
                new Sentence("atomik saatin 10 mhz sinyali kalibrasyon hizmetlerinde referans olarak kullanılmaktadır"),
                new Sentence("rehberimiz bu bölgedeki çıngıraklı yılan varlığı hakkında konuştu"),
                new Sentence("bu haksızlık da unutulup gitmişti"),
                new Sentence("4'lü tahıl zirvesi İstanbul'da gerçekleşti"),
                new Sentence("10'luk sistemden 100'lük sisteme geçiş yapılacak"),
                new Sentence("play-off maçlarına çıkacak takımlar belli oldu"),
                new Sentence("bu son model cihaz 24 inç ekran büyüklüğünde ve 9 kg ağırlıktadır")
            };
            Sentence[] modified =
            {
                new Sentence("bugünki ortögretim sisteminin oluşumu Cumhuriyet döneminde gerçekleşmiştir"),
                new Sentence("billinçiszce dağıtılan kredilerin sorun yaratmayacağını düşünmelerinin nedeni bankaların büyüklüğünden kaynaklanıyordu"),
                new Sentence("Yemen'de ekonomik durrğunlk nedeniyle yeni bir insani kriz yaşanabileceği uyaarisi yapıldı"),
                new Sentence("hafif ricarii araç aryerlere çarptı"),
                new Sentence("olayi akşam haberlerinde birinci haber olrak verdiler"),
                new Sentence("dünyada geçen hafta da iktida rları sallayan soyals çalkantılarla dolu geçti"),
                new Sentence("sırtı kara adındaki canlı , bir balıktır"),
                new Sentence("siyahayı , ayıgiller familyasına ait bir ayı türüdür"),
                new Sentence("yeni se zon başladı gibs"),
                new Sentence("alis veriş için markete gitit"),
                new Sentence("kucuk bri yalı çapkını gecti"),
                new Sentence("mes lek odaları birliği yendien toplandı"),
                new Sentence("yeniyılın sonrasında vakalarda artış oldu"),
                new Sentence("atomik saatin 10mhz sinyali kalibrasyon hizmetlerinde referans olarka kullanılmaktadır"),
                new Sentence("rehperimiz buı bölgedeki çıngıraklıyılan varlıgı hakkınd konustu"),
                new Sentence("bu haksızlıkda unutulup gitmişti"),
                new Sentence("4 lı tahıl zirvesi İstanbul'da gerçekleşti"),
                new Sentence("10 lük sistemden 100 lık sisteme geçiş yapılacak"),
                new Sentence("play - off maçlarına çıkacak takımlar belli oldu"),
                new Sentence("bu son model ciha 24inç ekran büyüklüğünde ve 9kg ağırlıktadır")
            };
            var fsm = new FsmMorphologicalAnalyzer();
            var nGram = new NGram<string>("../../../ngram.txt");
            nGram.CalculateNGramProbabilities(new NoSmoothing<string>());
            var contextBasedSpellChecker = new ContextBasedSpellChecker(fsm, nGram, new SpellCheckerParameter());
            for (var i = 0; i < modified.Length; i++)
            {
                Assert.AreEqual(original[i].ToString(), contextBasedSpellChecker.SpellCheck(modified[i]).ToString());
            }   
        }

        [Test]
        public void TestSpellCheckSurfaceForm()
        {
            var parameter = new SpellCheckerParameter();
            parameter.SetRootNGram(false);
            var fsm = new FsmMorphologicalAnalyzer();
            var nGram = new NGram<string>("../../../ngram.txt");
            nGram.CalculateNGramProbabilities(new NoSmoothing<string>());
            var contextBasedSpellChecker = new ContextBasedSpellChecker(fsm, nGram, parameter);
            Assert.AreEqual("noter hakkında", contextBasedSpellChecker.SpellCheck(new Sentence("noter hakkınad")).ToString());
            Assert.AreEqual("farklı olarak",
                contextBasedSpellChecker.SpellCheck(new Sentence("fatklı olrak")).ToString());
            Assert.AreEqual("yapılan anlaşma", contextBasedSpellChecker.SpellCheck(new Sentence("apıla anlaşma")).ToString());
        }
    }
}